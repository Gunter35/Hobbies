using Hobbies.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hobbies.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService _bookService)
        {
            bookService = _bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var model = await bookService.GetAllAsync();

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
