using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Comment;
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

        public async Task<GameDetailsViewModel> GameDetailsById(Guid id)
        {
            return await context.Games
                .Where(g => g.Id == id)
                .Include(g => g.Comments)
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Genre = g.Genre.Name,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    Name = g.Name,
                    Rating = g.Rating,
                    Creator = g.Creator,
                    Comments = g.Comments
                    .Select(c => new CommentViewModel()
                    {
                        Description = c.Description,
                        Id = c.Id
                    }).ToList()
                })
                .FirstAsync();
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

            if (entity == null)
            {
                throw new ArgumentException("Game not found");
            }

            entity.Rating = game.Rating;
            entity.Name = game.Name;
            entity.Description = game.Description;
            entity.Creator = game.Creator;
            entity.ImageUrl = game.ImageUrl;
            entity.GenreId = game.GenreId;

            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await context.Games
                .AnyAsync(g => g.Id == id);
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

        public async Task<EditGameViewModel> GetForEditAsync(Guid gameId)
        {
            var game = await context.Games.FindAsync(gameId);
            if (game == null)
            {
                throw new ArgumentException("Game not found");
            }

            var model = new EditGameViewModel()
            {
                Id = gameId,
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

        public async Task AddComment(Guid gameId, string comment)
        {
            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
            {
                throw new ArgumentException("Invalid game Id");
            }

            if (String.IsNullOrEmpty(comment))
            {
                throw new ArgumentException("Invalid comment");
            }

            var currComment = new Comment()
            {
                Description = comment,
                GameId = gameId,
                Game = game
            };

            game.Comments.Add(currComment);
            await context.Comments.AddAsync(currComment);
            await context.SaveChangesAsync();
        }
    }
}
