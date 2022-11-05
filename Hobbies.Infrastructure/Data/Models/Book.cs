using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Book;

namespace Hobbies.Infrastructure.Data.Models
{
    public class Book
    {
        /// <summary>
        /// Sets the Id of the Book
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Sets the Title of the Book
        /// </summary>
        [Required]
        [MaxLength(MaxBookTitle)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Sets the name of the Author of the Book
        /// </summary>
        [Required]
        [MaxLength(MaxBookAuthor)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(MaxBookDescription)]
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
        public BookGenre Genre { get; set; } = null!;

        /// <summary>
        /// Sets the connection between Book and UsersBooks tables
        /// </summary>
        public ICollection<UserBook> UsersBooks { get; set; } = new List<UserBook>();
    }
}
