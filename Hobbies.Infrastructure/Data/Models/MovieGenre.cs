using System.ComponentModel.DataAnnotations;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.MovieGenre;

namespace Hobbies.Infrastructure.Data.Models
{
    public class MovieGenre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MaxGenreName)]
        public string Name { get; set; } = null!;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
