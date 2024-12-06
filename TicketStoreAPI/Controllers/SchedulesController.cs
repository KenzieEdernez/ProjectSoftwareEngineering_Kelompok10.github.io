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
            List<ScheduleResponse> response = new List<ScheduleResponse>();

            if (!schedules.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No schedules found."
                });
            }

            foreach (Schedule s in schedules)
            {
                var temp = new ScheduleResponse
                {
                    MovieId = s.MovieId,
                    Price = s.Price,
                    SchedulesId = s.ScheduleId,
                    ShowTime = s.ShowTime,
                    TheaterId = s.TheaterId
                };
                response.Add(temp);
            }

            return Ok(new ResponseModel<IEnumerable<ScheduleResponse>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
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
            var response = new ScheduleResponse
            {
                MovieId = schedule.MovieId,
                Price = schedule.Price,
                SchedulesId = schedule.ScheduleId,
                ShowTime = schedule.ShowTime,
                TheaterId = schedule.TheaterId
            };

            return Ok(new ResponseModel<ScheduleResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpGet("Upcoming")]
        public async Task<ActionResult<ResponseModel<object>>> GetUpcomingSchedules()
        {
            var currentDateTime = DateTime.UtcNow;
            var upcomingSchedules = await _context.Schedules
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .Where(s => s.ShowTime > currentDateTime)
                .ToListAsync();

            List<ScheduleResponse> response = new List<ScheduleResponse>();

            if (!upcomingSchedules.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No upcoming movie found."
                });
            }

            foreach (Schedule s in upcomingSchedules)
            {
                var temp = new ScheduleResponse
                {
                    MovieId = s.MovieId,
                    Price = s.Price,
                    SchedulesId = s.ScheduleId,
                    ShowTime = s.ShowTime,
                    TheaterId = s.TheaterId
                };
                response.Add(temp);
            }

            return Ok(new ResponseModel<IEnumerable<ScheduleResponse>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpGet("Today")]
        public async Task<ActionResult<ResponseModel<object>>> GetTodaySchedules()
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);
            var todaysSchedules = await _context.Schedules
                .Include(s => s.Movie)
                .Include(s => s.Theater)
                .Where(s => s.ShowTime >= today && s.ShowTime < tomorrow)
                .ToListAsync();

            List<ScheduleResponse> response = new List<ScheduleResponse>();

            if (!todaysSchedules.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No Movies playing today."
                });
            }

            foreach (Schedule s in todaysSchedules)
            {
                var temp = new ScheduleResponse
                {
                    MovieId = s.MovieId,
                    Price = s.Price,
                    SchedulesId = s.ScheduleId,
                    ShowTime = s.ShowTime,
                    TheaterId = s.TheaterId
                };
                response.Add(temp);
            }

            return Ok(new ResponseModel<IEnumerable<ScheduleResponse>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = "Schedule added successfully"
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
