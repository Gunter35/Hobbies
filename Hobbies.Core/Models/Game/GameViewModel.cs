using System.ComponentModel.DataAnnotations;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Game;

namespace Hobbies.Core.Models.Game
{
    public class GameViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(MaxGameName, MinimumLength = MinGameName)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(MaxGameCreator, MinimumLength = MinGameCreator)]
        public string Creator { get; set; } = null!;

        [Required]
        [StringLength(MaxGameDescription, MinimumLength = MinGameDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        [Required]
        public string? Genre { get; set; }
    }
}
