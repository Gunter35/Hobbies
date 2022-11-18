using Microsoft.AspNetCore.Identity;

namespace Hobbies.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserBook> UsersBooks { get; set; } = new List<UserBook>();
        public ICollection<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
