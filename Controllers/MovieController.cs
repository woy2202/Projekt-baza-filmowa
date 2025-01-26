using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FilmowaBaza.Models;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private static readonly List<Movie> Movies = new();

    [HttpGet]
    public IActionResult GetAllMovies()
    {
        try
        {
            return Ok(Movies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Wystąpił błąd serwera.", details = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Nieprawidłowe dane filmu.", errors = ModelState });
            }

            movie.Id = Movies.Count > 0 ? Movies.Max(m => m.Id) + 1 : 1;
            Movies.Add(movie);

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Wystąpił błąd serwera.", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        try
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound(new { message = "Nie znaleziono filmu." });
            }

            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Wystąpił błąd serwera.", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Nieprawidłowe dane filmu.", errors = ModelState });
            }

            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound(new { message = "Nie znaleziono filmu do aktualizacji." });
            }

            movie.Title = updatedMovie.Title;
            movie.ReleaseYear = updatedMovie.ReleaseYear;
            movie.Director = updatedMovie.Director;
            movie.Genre = updatedMovie.Genre;
            movie.Country_of_origin = updatedMovie.Country_of_origin;
            movie.PosterUrl = updatedMovie.PosterUrl;

            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Wystąpił błąd serwera.", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        try
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound(new { message = "Nie znaleziono filmu do usunięcia." });
            }

            Movies.Remove(movie);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Wystąpił błąd serwera.", details = ex.Message });
        }
    }
}
