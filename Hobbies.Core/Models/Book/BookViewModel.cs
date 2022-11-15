using System.ComponentModel.DataAnnotations;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Book;

namespace Hobbies.Core.Models.Book
{
    public class BookViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(MaxBookTitle, MinimumLength = MinBookTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxBookAuthor, MinimumLength = MinBookAuthor)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(MaxBookDescription, MinimumLength = MinBookDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        [Required]
        public string? Genre { get; set; }
    }
}
