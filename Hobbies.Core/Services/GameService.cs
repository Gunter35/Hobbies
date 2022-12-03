using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Game;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hobbies.Core.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext context;

        public GameService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddGameAsync(AddGameViewModel game)
        {
            var entity = new Game()
            {
                Name = game.Name,
                Creator = game.Creator,
                ImageUrl = game.ImageUrl,
                Description = game.Description,
                GenreId = game.GenreId,
                Rating = game.Rating
            };

            await context.Games.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddGameToCollectionAsync(Guid gameId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(ug => ug.UsersGames)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null)
            {
                throw new ArgumentException("Invalid game Id");
            }

            if (!user.UsersGames.Any(g => g.GameId == gameId))
            {
                user.UsersGames.Add(new UserGame()
                {
                    Game = game,
                    GameId = gameId,
                    User = user,
                    UserId = userId
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var game = await context.Games.FirstOrDefaultAsync(game => game.Id == id);

            if (game == null)
            {
                throw new ArgumentException("Invalid game Id");
            }

            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(EditGameViewModel game)
        {
            var entity = await context.Games.FindAsync(game.Id);

            entity.Rating = game.Rating;
            entity.Name = game.Name;
            entity.Description = game.Description;
            entity.Creator = game.Creator;
            entity.ImageUrl = game.ImageUrl;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameViewModel>> GetAllAsync()
        {
            var entities = await context.Games
                .Include(g => g.Genre)
                .ToListAsync();

            return entities
                .Select(g => new GameViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    Creator = g.Creator,
                    Genre = g?.Genre.Name,
                    Rating = g.Rating,
                    ImageUrl = g.ImageUrl
                });
        }

        public async Task<EditGameViewModel> GetForEditAsync(Guid id)
        {
            var game = await context.Games.FindAsync(id);
            var model = new EditGameViewModel()
            {
                Id = id,
                Creator = game.Creator,
                Description = game.Description,
                Rating = game.Rating,
                ImageUrl = game.ImageUrl,
                GenreId = game.GenreId,
                Name = game.Name
            };

            model.Genres = await GetGenresAsync();

            return model;
        }

        public async Task<IEnumerable<GameGenre>> GetGenresAsync()
        {
            return await context.GamesGenres.ToListAsync();
        }

        public async Task<IEnumerable<GameViewModel>> GetMineAsync(string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGames)
                .ThenInclude(ug => ug.Game)
                .ThenInclude(g => g.Genre)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            return user.UsersGames
                .Select(g => new GameViewModel()
                {
                    Creator = g.Game.Creator,
                    Genre = g.Game.Genre?.Name,
                    Id = g.GameId,
                    ImageUrl = g.Game.ImageUrl,
                    Rating = g.Game.Rating,
                    Name = g.Game.Name,
                    Description = g.Game.Description
                });

        }

        public async Task RemoveGameFromCollectionAsync(Guid gameId, string userId)
        {
            var user = await context.Users
             .Where(u => u.Id == userId)
             .Include(u => u.UsersGames)
             .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var game = user.UsersGames.FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                user.UsersGames.Remove(game);
                await context.SaveChangesAsync();
            }
        }
    }
}
