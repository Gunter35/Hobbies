using Hobbies.Core.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Core.Models.Game
{
    public class GameDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Creator { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public string? Genre { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
