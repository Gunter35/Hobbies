using Hobbies.Core.Models.Admin.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Core.Contracts.Admin
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> AllUsersAsync();
    }
}
