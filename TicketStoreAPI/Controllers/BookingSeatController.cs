using Microsoft.AspNetCore.Mvc;
using TicketStoreAPI.Models;
using TicketStoreAPI.Data;
using Microsoft.EntityFrameworkCore;
using TicketStoreAPI.Models.request;
using TicketStoreAPI.Models.Response;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TicketStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSeatsController : ControllerBase
    {
        private readonly TicketStoreAPIContext _context;

        public BookingSeatsController(TicketStoreAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<object>>> GetBookingSeats()
        {
            var userIdString = User.FindFirstValue("name");

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Please login again."
                });
            }

            var bookingSeats = await _context.BookingSeats
                .Include(bs => bs.Booking)
                .Include(bs => bs.Seat)
                .ToListAsync();

            if (!bookingSeats.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No booking seats found."
                });
            }

            return Ok(new ResponseModel<IEnumerable<BookingSeat>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = bookingSeats
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> GetBookingSeat(int id)
        {
            var bookingSeat = await _context.BookingSeats
                .Include(bs => bs.Booking)
                .Include(bs => bs.Seat)
                .FirstOrDefaultAsync(bs => bs.SeatId == id);

            if (bookingSeat == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Booking seat with ID {id} not found."
                });
            }

            return Ok(new ResponseModel<BookingSeat>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = bookingSeat
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<BookingSeat>>> PostBookingSeat(BookingSeatCreateDTO dto)
        {
            if (!await _context.Bookings.AnyAsync(b => b.BookingId == dto.BookingId) ||
                !await _context.Seats.AnyAsync(s => s.SeatId == dto.SeatId))
            {
                return BadRequest(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Invalid booking or seat reference."
                });
            }

            if (await _context.BookingSeats.AnyAsync(bs => bs.SeatId == dto.SeatId)){
                return Conflict(new ResponseModel<string>{
                    StatusCode = StatusCodes.Status409Conflict,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Seat has been occupied."
                });
            }

            var bookingSeat = new BookingSeat
            {
                BookingId = dto.BookingId,
                SeatId = dto.SeatId
            };

            _context.BookingSeats.Add(bookingSeat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingSeat), new { id = bookingSeat.SeatId }, new ResponseModel<BookingSeat>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = bookingSeat
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteBookingSeat(int id)
        {
            var bookingSeat = await _context.BookingSeats.FindAsync(id);

            if (bookingSeat == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Booking seat with ID {id} not found."
                });
            }

            _context.BookingSeats.Remove(bookingSeat);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = "Booking seat deleted successfully."
            });
        }
    }
}
