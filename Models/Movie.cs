using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FilmowaBaza.Models
{
    public class Movie
    {
        public int Id { get; set; } // Id filmu

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } // Tytuł filmu

        [Required]
        public int ReleaseYear { get; set; } // Rok produkcji

        [Required]
        [MaxLength(100)]
        public string Director { get; set; } // Reżyser

        [MaxLength(50)]
        public string Genre { get; set; } // Gatunek
        [MaxLength(50)]
        public string Country_of_origin { get; set; } // Kraj produkcji 
    }
    [XmlRoot("Movies")]
    public class MovieList
    {
        [XmlElement("Movie")]
        public List<Movie> Movies { get; set; }
    }
}


