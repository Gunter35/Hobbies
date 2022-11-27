using Microsoft.AspNetCore.Mvc;

namespace Hobbies.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
