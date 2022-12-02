using Hobbies.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Core.Models.Game
{
    public class EditGameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Creator { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public Guid GenreId { get; set; }

        public IEnumerable<GameGenre> Genres { get; set; } = new List<GameGenre>();
    }
}
