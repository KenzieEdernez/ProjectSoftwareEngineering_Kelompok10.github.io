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
            List<TheaterResponse> response = new List<TheaterResponse>();

            if (!theaters.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No theaters found."
                });
            }

            foreach (Theater t in theaters)
            {
                var temp = new TheaterResponse
                {
                    Capacity = t.Capacity,
                    TheatersName = t.Name,
                    TheatersId = t.TheaterId
                };
                response.Add(temp);
            }

            return Ok(new ResponseModel<IEnumerable<TheaterResponse>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
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

            var response = new TheaterResponse
            {
                Capacity = theater.Capacity,
                TheatersName = theater.Name,
                TheatersId = theater.TheaterId
            };

            return Ok(new ResponseModel<TheaterResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<Theater>>> PostTheater(TheaterCreateDTO dto)
        {
            var theater = new Theater
            {
                Name = dto.TheaterName,
                Capacity = dto.Capacity
            };

            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = "Theater added successfully"
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
