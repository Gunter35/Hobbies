using Hobbies.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Core.Models.Book
{
    public class EditBookViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public Guid GenreId { get; set; }

        public IEnumerable<BookGenre> Genres { get; set; } = new List<BookGenre>();
    }
}
