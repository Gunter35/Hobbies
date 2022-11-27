using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Game;
using Hobbies.Infrastructure.Data;
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
    }
}
