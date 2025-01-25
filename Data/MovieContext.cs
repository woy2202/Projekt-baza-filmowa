using Microsoft.EntityFrameworkCore;
using FilmowaBaza.Models;

namespace FilmowaBaza.Data
{
    public class MovieContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
    }
}
