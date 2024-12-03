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
    public class SchedulesController : ControllerBase
    {
        private readonly TicketStoreAPIContext _context;

        public SchedulesController(TicketStoreAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<object>>> GetSchedules()
        {
            var schedules = await _context.Schedules
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .ToListAsync();

            if (!schedules.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No schedules found."
                });
            }

            return Ok(new ResponseModel<IEnumerable<Schedule>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = schedules
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .FirstOrDefaultAsync(s => s.ScheduleId == id);

            if (schedule == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Schedule with ID {id} not found."
                });
            }

            return Ok(new ResponseModel<Schedule>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = schedule
            });
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<ResponseModel<Schedule>>> PostSchedule(ScheduleCreateDTO dto)
        {
            if (!await _context.Movies.AnyAsync(m => m.MovieId == dto.MovieId) ||
                !await _context.Theaters.AnyAsync(t => t.TheaterId == dto.TheaterId))
            {
                return BadRequest(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Invalid movie or theater reference."
                });
            }

            var schedule = new Schedule
            {
                MovieId = dto.MovieId,
                TheaterId = dto.TheaterId,
                ShowTime = dto.ShowTime,
                Price = dto.Price
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.ScheduleId }, new ResponseModel<Schedule>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = schedule
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Schedule with ID {id} not found."
                });
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = "Schedule deleted successfully."
            });
        }
    }
}
