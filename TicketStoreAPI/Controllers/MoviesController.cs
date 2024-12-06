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
            List<MovieResponse> response = new List<MovieResponse>();

            if (!movies.Any())
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "No movies found."
                });
            }

            foreach (Movie mov in movies)
            {
                var temp = new MovieResponse
                {
                    Title = mov.Title,
                    Genre = mov.Genre,
                    Duration = mov.Duration,
                    Rating = mov.Rating,
                    Description = mov.Description,
                    PosterUrl = mov.PosterUrl,
                    ReleaseDate = mov.ReleaseDate
                };
                response.Add(temp);
            }

            return Ok(new ResponseModel<IEnumerable<MovieResponse>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<Movie>>> PostMovie(MovieCreatesDto dto)
        {
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

            return Ok(new ResponseModel<string>
            {
                StatusCode = StatusCodes.Status201Created,
                RequestMethod = HttpContext.Request.Method,
                Data = "Movie added successfully"
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> GetMovie(int id)
        {
            var movie = await _context.Movies.Where(m => m.MovieId == id).FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Data = $"Movie with ID {id} not found."
                });
            }
            var response = new MovieResponse
            {
                Description = movie.Description,
                Duration = movie.Duration,
                Genre = movie.Genre,
                MoviesId = movie.MovieId,
                PosterUrl = movie.PosterUrl,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title,
            };

            return Ok(new ResponseModel<MovieResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = response
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
