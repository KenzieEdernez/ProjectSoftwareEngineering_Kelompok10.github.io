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
    public class SeatsController : ControllerBase
    {
        private readonly TicketStoreAPIContext _context;

        public SeatsController(TicketStoreAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<object>>> GetSeats()
        {
            var seats = await _context.Seats
                .Include(s => s.Theater)
                .ToListAsync();
            List<SeatResponse> response = new List<SeatResponse>();
            if (!seats.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No seats found."
                });
            }

            foreach (Seat s in seats)
            {
                var temp = new SeatResponse
                {
                    SeatNumber = s.SeatNumber,
                    TheaterId = s.TheaterId,
                    SeatsId = s.SeatId
                };
                response.Add(temp);
            }

            return Ok(new ResponseModel<IEnumerable<SeatResponse>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> GetSeat(int id)
        {
            var seat = await _context.Seats
                .Include(s => s.Theater)
                .FirstOrDefaultAsync(s => s.SeatId == id);

            if (seat == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Seat with ID {id} not found."
                });
            }

            var response = new SeatResponse
            {
                SeatNumber = seat.SeatNumber,
                SeatsId = seat.SeatId,
                TheaterId = seat.TheaterId
            };

            return Ok(new ResponseModel<SeatResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<Seat>>> PostSeat(SeatCreateDTO dto)
        {
            if (!await _context.Theaters.AnyAsync(t => t.TheaterId == dto.TheaterId))
            {
                return BadRequest(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Invalid theater reference."
                });
            }

            var seat = new Seat
            {
                TheaterId = dto.TheaterId,
                SeatNumber = dto.SeatNumber
            };

            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = "Seat added successfully"
            });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteSeat(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Seat with ID {id} not found."
                });
            }

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = "Seat deleted successfully."
            });
        }
    }
}
