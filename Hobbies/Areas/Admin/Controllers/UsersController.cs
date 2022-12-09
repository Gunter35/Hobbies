using Hobbies.Core.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Hobbies.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> All()
        {
            var model = await userService.AllUsersAsync();

            return View(model);
        }
    }
}
