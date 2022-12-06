using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hobbies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var model = await movieService.GetAllAsync();

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var model = new AddMovieViewModel()
                {
                    Genres = await movieService.GetGenresAsync()
                };

                return View(model);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await movieService.AddMovieAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        public async Task<IActionResult> AddToCollection(Guid movieId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await movieService.AddMovieToCollectionAsync(movieId, userId);
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

                var model = await movieService.GetMineAsync(userId);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveFromCollection(Guid movieId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await movieService.RemoveMovieFromCollectionAsync(movieId, userId);

            return RedirectToAction(nameof(Mine));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid movieId)
        {
            var model = await movieService.GetForEditAsync(movieId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("All", "Movies");
            }

            await movieService.EditAsync(model);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid movieId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "User");
            }

            await movieService.DeleteAsync(movieId);
            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid movieId)
        {
            if ((await movieService.Exists(movieId)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await movieService.MovieDetailsById(movieId);

            return View(model);
        }

        public async Task<IActionResult> AddComment(Guid movieId, [FromForm] string comment)
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
                await movieService.AddComment(movieId, comment);

                return RedirectToAction("Details", "Movies", new { @movieId = movieId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User");
            }

        }
    }
}
