using Hobbies.Core.Services.Admin.User;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.UnitTests.Users
{
    public class UsersServiceTest
    {
        [Test]
        public async Task CheckingIfAllUsersWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var userService = new UserService(dbContext);

            var user = new User()
            {
                Id = "1",
                UserName = "Pesho",
                Email = "pesho@gmail.com"
            };

            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var users = userService.AllUsersAsync();

            Assert.AreEqual(1, users.Result.Count());
        }
    }
}
