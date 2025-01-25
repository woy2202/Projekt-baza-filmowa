using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FilmowaBaza.Data;
using FilmowaBaza.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FilmowaBaza.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MovieController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        [HttpPost]

        public JsonResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }


            _movieContext.Movies.Add(movie);

            _movieContext.SaveChanges();

            return new JsonResult(Ok(movie));


        }
        [HttpGet("/all")]
        public JsonResult GetAll()
        {

            var result = _movieContext.Movies.ToList();

            return new JsonResult(Ok(result));

        }
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _movieContext.Movies.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));


        }
        [HttpPut("{id}")]

        public JsonResult Edit(int id, Movie movie)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            var movieInDb = _movieContext.Movies.Find(movie.Id);

            if (movieInDb == null)
                return new JsonResult(NotFound());

            movieInDb.Title = movie.Title;
            movieInDb.ReleaseYear = movie.ReleaseYear;
            movieInDb.Director = movie.Director;
            movieInDb.Genre = movie.Genre;
            movieInDb.Country_of_origin = movie.Country_of_origin;

            _movieContext.SaveChanges();
            return new JsonResult(Ok(movie));

        }
        [HttpDelete]

        public JsonResult Delete(int id)
        {
            var result = _movieContext.Movies.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _movieContext.Movies.Remove(result);

            _movieContext.SaveChanges();
            return new JsonResult(Ok(result));


        }
    }
}
