using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Book;
using Hobbies.Core.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var model = new AddBookViewModel()
                {
                    Genres = await bookService.GetGenresAsync()
                };

                return View(model);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bookService.AddBookAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        public async Task<IActionResult> AddToCollection(Guid bookId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await bookService.AddBookToCollectionAsync(bookId, userId);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> Mine()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var model = await bookService.GetMineAsync(userId);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveFromCollection(Guid bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await bookService.RemoveBookFromCollectionAsync(bookId, userId);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid bookId)
        {
            var model = await bookService.GetForEditAsync(bookId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("All", "Books");
            }

            await bookService.EditAsync(model);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid bookId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "User");
            }

            await bookService.DeleteAsync(bookId);
            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid bookId)
        {
            if ((await bookService.Exists(bookId)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await bookService.BookDetailsById(bookId);

            return View(model);
        }

        public async Task<IActionResult> AddComment(Guid bookId, [FromForm] string comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException("Something went wrong...");
                }
                if (comment == null || comment.Contains('<') || comment.Contains("1=1"))
                {
                    throw new ArgumentException("Invalid comment!");
                }
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await bookService.AddComment(bookId, comment);

                return RedirectToAction("Details", "Books", new { @bookId = bookId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User");
            }
            
        }
    }

}
