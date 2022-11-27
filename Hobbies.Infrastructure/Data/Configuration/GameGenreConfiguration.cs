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
    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.HasData(SeedGamesGenres());
        }

        private List<GameGenre> SeedGamesGenres()
        {
            var genres = new List<GameGenre>();

            var genre1 = new GameGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Platform"
            };
            genres.Add(genre1);

            var genre2 = new GameGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Shooter"
            };
            genres.Add(genre2);

            var genre3 = new GameGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Fighting"
            };
            genres.Add(genre3);

            var genre4 = new GameGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Survival"
            };
            genres.Add(genre4);

            var genre5 = new GameGenre()
            {
                Id = Guid.NewGuid(),
                Name = "Battle Royale"
            };
            genres.Add(genre5);

            return genres;
        }
    }
}
