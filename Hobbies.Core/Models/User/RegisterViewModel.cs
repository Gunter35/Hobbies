using System.ComponentModel.DataAnnotations;
using static Hobbies.Infrastructure.Data.Constants.DataConstants.User;

namespace Hobbies.Core.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(MaxUserUsername, MinimumLength = MinUserUsername)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(MaxUserEmail, MinimumLength = MinUserEmail)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(MaxUserPassword, MinimumLength = MinUserPassword)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
