using Hobbies.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Movie;

namespace Hobbies.Core.Models.Movie
{
    public class MovieViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(MaxMovieTitle, MinimumLength = MinMovieTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxMovieDirector, MinimumLength = MinMovieDirector)]
        public string Director { get; set; } = null!;

        [Required]
        [StringLength(MaxMovieDescription, MinimumLength = MinMovieDirector)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        [Required]
        public string? Genre { get; set; }
    }
}
