﻿using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Game;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
