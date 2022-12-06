using Hobbies.Infrastructure.Data.Configuration;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hobbies.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BooksGenres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GamesGenres { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserBookConfiguration());
            builder.ApplyConfiguration(new UserMovieConfiguration());
            builder.ApplyConfiguration(new UserGameConfiguration());
            builder.ApplyConfiguration(new BookGenreConfiguration());
            builder.ApplyConfiguration(new MovieGenreConfiguration());
            builder.ApplyConfiguration(new GameGenreConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}