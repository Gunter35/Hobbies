using System.ComponentModel.DataAnnotations;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.GameGenre;

namespace Hobbies.Infrastructure.Data.Models
{
    public class GameGenre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MaxGenreName)]
        public string Name { get; set; } = null!;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
