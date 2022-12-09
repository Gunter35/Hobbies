using Hobbies.Core.Contracts.Admin;
using Hobbies.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Hobbies.Core.Models.Admin.User;

namespace Hobbies.Core.Services.Admin.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<UserViewModel>> AllUsersAsync()
        {
            List<UserViewModel> users;

            users = await context.Users
                .Select(x => new UserViewModel()
                {
                    UserName = x.UserName,
                    Email = x.Email
                })
                .ToListAsync();

            return users;
        }

    }
}
