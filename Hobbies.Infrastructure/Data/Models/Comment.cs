using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.Book;

namespace Hobbies.Infrastructure.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MaxBookDescription)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public Guid? BookId { get; set; }
        public Book? Book { get; set; }

        [ForeignKey(nameof(Movie))]
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }

        [ForeignKey(nameof(Game))]
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }
    }
}
