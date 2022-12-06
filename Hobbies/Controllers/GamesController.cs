using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hobbies.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IGameService gameService;

        public GamesController(IGameService _gameService)
        {
            gameService = _gameService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var model = await gameService.GetAllAsync();

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                var model = new AddGameViewModel()
                {
                    Genres = await gameService.GetGenresAsync()
                };

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await gameService.AddGameAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                throw;
            }
        }

        public async Task<IActionResult> AddToCollection(Guid gameId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await gameService.AddGameToCollectionAsync(gameId, userId);
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

                var model = await gameService.GetMineAsync(userId);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveFromCollection(Guid gameId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await gameService.RemoveGameFromCollectionAsync(gameId, userId);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid gameId)
        {
            var model = await gameService.GetForEditAsync(gameId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Game not found!");
                return RedirectToAction("All", "Games");
            }

            await gameService.EditAsync(model);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid gameId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "User");
            }

            await gameService.DeleteAsync(gameId);
            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid gameId)
        {
            if ((await gameService.Exists(gameId)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await gameService.GameDetailsById(gameId);

            return View(model);
        }

        public async Task<IActionResult> AddComment(Guid gameId, [FromForm] string comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException("Something went wrong...");
                }
                if (comment == null)
                {
                    throw new ArgumentException("Invalid comment!");
                }
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await gameService.AddComment(gameId, comment);

                return RedirectToAction("Details", "Games", new { @gameId = gameId });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
