using Hobbies.Infrastructure.Data.Models;

namespace Hobbies.Core.Models.Movie
{
    public class EditMovieViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public Guid GenreId { get; set; }

        public IEnumerable<MovieGenre> Genres { get; set; } = new List<MovieGenre>();
    }
}
