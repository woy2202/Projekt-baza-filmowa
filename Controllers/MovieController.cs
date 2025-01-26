using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmowaBaza.Data;
using FilmowaBaza.Models;

namespace FilmowaBaza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/movies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("Movie ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieInDb = await _context.Movies.FindAsync(id);

            if (movieInDb == null)
            {
                return NotFound();
            }

            movieInDb.Title = movie.Title;
            movieInDb.ReleaseYear = movie.ReleaseYear;
            movieInDb.Director = movie.Director;
            movieInDb.Genre = movie.Genre;
            movieInDb.Country_of_origin = movie.Country_of_origin;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/movies/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }
    }
}
