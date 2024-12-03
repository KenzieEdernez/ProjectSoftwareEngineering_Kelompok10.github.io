using Microsoft.AspNetCore.Mvc;
using TicketStoreAPI.Data;
using TicketStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using TicketStoreAPI.Models.request;
using TicketStoreAPI.Models.Response;
using Microsoft.AspNetCore.Authorization;

namespace TicketStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly TicketStoreAPIContext _context;

        public TheatersController(TicketStoreAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<object>>> GetTheaters()
        {
            var theaters = await _context.Theaters.ToListAsync();

            if (!theaters.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No theaters found."
                });
            }

            return Ok(new ResponseModel<IEnumerable<Theater>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = theaters
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> GetTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Theater with ID {id} not found."
                });
            }

            return Ok(new ResponseModel<Theater>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = theater
            });
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<ResponseModel<Theater>>> PostTheater(TheaterCreateDTO dto)
        {
            var theater = new Theater
            {
                Name = dto.TheaterName,
                Capacity = dto.Capacity
            };

            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTheater), new { id = theater.TheaterId }, new ResponseModel<Theater>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = theater
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);

            if (theater == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Theater with ID {id} not found."
                });
            }

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = "Theater deleted successfully."
            });
        }
    }
}
