using Microsoft.AspNetCore.Mvc;
using TicketStoreAPI.Data;
using TicketStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using TicketStoreAPI.Models.request;
using TicketStoreAPI.Models.Response;

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

            if (!seats.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No seats found."
                });
            }

            return Ok(new ResponseModel<IEnumerable<Seat>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = seats
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

            return Ok(new ResponseModel<Seat>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = seat
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<Seat>>> PostSeat(SeatCreateDTO dto)
        {
            if (!await _context.Theaters.AnyAsync(t => t.TheatersId == dto.TheaterId))
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

            return CreatedAtAction(nameof(GetSeat), new { id = seat.SeatId }, new ResponseModel<SeatResponse>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = new SeatResponse{
                    SeatsId = seat.SeatId,
                    TheaterId = seat.TheaterId,
                    SeatNumber = seat.SeatNumber
                }
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> PutSeat(int id, Seat dto)
        {
            if (id != dto.SeatId)
            {
                return BadRequest(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Seat ID mismatch."
                });
            }

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

            seat.TheaterId = dto.TheaterId;
            seat.SeatNumber = dto.SeatNumber;

            _context.Entry(seat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = "Seat updated successfully."
            });
        }

        [HttpDelete("{id}")]
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
