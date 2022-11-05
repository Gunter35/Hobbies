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
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasData(SeedMoviessGenres());
        }

        private List<MovieGenre> SeedMoviessGenres()
        {
            var genres = new List<MovieGenre>();

            var genre1 = new MovieGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Action"
            };
            genres.Add(genre1);

            var genre2 = new MovieGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Comedy"
            };
            genres.Add(genre2);

            var genre3 = new MovieGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Drama"
            };
            genres.Add(genre3);

            var genre4 = new MovieGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Fantasy"
            };
            genres.Add(genre4);

            var genre5 = new MovieGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Horror"
            };
            genres.Add(genre5);

            return genres;
        }
    }
}
