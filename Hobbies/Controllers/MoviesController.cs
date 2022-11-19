using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Movie;
using Microsoft.AspNetCore.Mvc;

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
    }
}
