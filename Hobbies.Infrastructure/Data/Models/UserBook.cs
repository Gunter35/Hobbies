using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hobbies.Infrastructure.Data.Models
{
    public class UserBook
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]  
        [ForeignKey(nameof(Book))]  
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
