using Microsoft.AspNetCore.Mvc;
using TicketStoreAPI.Models;
using TicketStoreAPI.Data;
using Microsoft.EntityFrameworkCore;
using TicketStoreAPI.Models.request;
using TicketStoreAPI.Models.Response;
using Microsoft.AspNetCore.Authorization;

namespace TicketStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly TicketStoreAPIContext _context;

        public MoviesController(TicketStoreAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseModel<object>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();

            if (!movies.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No movies found."
                });
            }

            return Ok(new ResponseModel<IEnumerable<Movie>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = movies
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<Movie>>> PostMovie(MovieCreatesDto dto)
        {
            // Validate the incoming movie data (if necessary)
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Genre))
            {
                return BadRequest(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Invalid movie data."
                });
            }

            var movie = new Movie
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Duration = dto.Duration,
                Rating = dto.Rating,
                Description = dto.Description,
                PosterUrl = dto.PosterUrl,
                ReleaseDate = dto.ReleaseDate
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovies), new { id = movie.MoviesId }, new ResponseModel<Movie>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = movie
            });
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"movies with ID {id} not found."
                });
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = "Movie deleted successfully."
            });
        }

    }
}
