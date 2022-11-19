using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Movie;
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
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext context;

        public MovieService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddMovieAsync(AddMovieViewModel movie)
        {
            var entity = new Movie()
            {
                Title = movie.Title,
                Director = movie.Director,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                Rating = movie.Rating,
                GenreId = movie.GenreId
            };

            await context.Movies.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddMovieToCollectionAsync(Guid movieId, string userId)
        {
            var user = context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid movie Id");
            }

        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var entities = await context.Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return entities
                .Select(m => new MovieViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Genre = m?.Genre.Name
                });
        }

        public async Task<IEnumerable<MovieGenre>> GetGenresAsync()
        {
            return await context.MoviesGenres.ToListAsync();
        }
    }
}
