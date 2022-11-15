using System.ComponentModel.DataAnnotations;

namespace Hobbies.Core.Models.User
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
