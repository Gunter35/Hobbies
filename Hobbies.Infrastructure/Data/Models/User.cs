using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserBook> UsersBooks { get; set; } = new List<UserBook>();
    }
}
