using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Infrastructure.Data.Configuration
{
    public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder.HasData(SeedBooksGenres());
        }

        private List<BookGenre> SeedBooksGenres()
        {
            var genres = new List<BookGenre>();

            var genre1 = new BookGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Fantasy"
            };
            genres.Add(genre1);

            var genre2 = new BookGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Adventure"
            };
            genres.Add(genre2);

            var genre3 = new BookGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Romance"
            };
            genres.Add(genre3);

            var genre4 = new BookGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Horror"
            };
            genres.Add(genre4);

            var genre5 = new BookGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Development"
            };
            genres.Add(genre5);

            return genres;
        }
    }
}
