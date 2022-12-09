using Microsoft.AspNetCore.Mvc;

namespace Hobbies.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
