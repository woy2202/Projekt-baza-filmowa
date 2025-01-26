using System.ComponentModel.DataAnnotations;

namespace FilmowaBaza.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        [MaxLength(100)]
        public string Director { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [MaxLength(50)]
        public string Country_of_origin { get; set; }

        [MaxLength(255)]
        public string PosterUrl { get; set; }
    }
}
