using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Game;

namespace Hobbies.Infrastructure.Data.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Sets the Title of the Book
        /// </summary>
        [Required]
        [MaxLength(MaxGameName)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Sets the name of the Author of the Book
        /// </summary>
        [Required]
        [MaxLength(MaxGameCreator)]
        public string Creator { get; set; } = null!;

        [Required]
        [MaxLength(MaxGameDescription)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Sets url of image
        /// </summary>
        [Required]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Sets the Rating of the Book
        /// </summary>
        [Required]
        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Sets the connection between Book and BookGenre tables
        /// </summary>
        [Required]
        [ForeignKey(nameof(Genre))]
        public Guid GenreId { get; set; }
        public GameGenre Genre { get; set; } = null!;

        /// <summary>
        /// Sets the connection between Book and UsersBooks tables
        /// </summary>
        public ICollection<UserGame> UsersGames { get; set; } = new List<UserGame>();
    }
}
