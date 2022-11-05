using System.ComponentModel.DataAnnotations;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.BookGenre;

namespace Hobbies.Infrastructure.Data.Models
{
    public class BookGenre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MaxGenreName)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
