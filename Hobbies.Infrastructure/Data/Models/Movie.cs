using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Movie;

namespace Hobbies.Infrastructure.Data.Models
{
    public class Movie
    { /// <summary>
      /// Sets the Id of the Movie
      /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Sets the Title of the Movie
        /// </summary>
        [Required]
        [MaxLength(MaxMovieTitle)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Sets the name of the Director of the Movie
        /// </summary>
        [Required]
        [MaxLength(MaxMovieDirector)]
        public string Director { get; set; } = null!;

        [Required]
        [MaxLength(MaxMovieDescription)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Sets url of image
        /// </summary>
        [Required]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Sets the Rating of the Movie
        /// </summary>
        [Required]
        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Sets the connection between Movie and MovieGenre tables
        /// </summary>
        [Required]
        [ForeignKey(nameof(Genre))]
        public Guid GenreId { get; set; }
        public MovieGenre Genre { get; set; } = null!;

        /// <summary>
        /// Sets the connection between Movie and UsersMovies tables
        /// </summary>
        public ICollection<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
