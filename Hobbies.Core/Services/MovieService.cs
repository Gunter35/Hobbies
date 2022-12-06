using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Comment;
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
            var user = await context.Users
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

            if (!user.UsersMovies.Any(m => m.MovieId == movieId))
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    MovieId = movieId,
                    UserId = userId,
                    Movie = movie,
                    User = user
                });
            }

            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

            if (movie == null)
            {
                throw new ArgumentException("Invalid game Id");
            }

            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(EditMovieViewModel movie)
        {
            var entity = await context.Movies.FindAsync(movie.Id);

            if (entity == null)
            {
                throw new ArgumentException("Movie not found");
            }

            entity.Rating = movie.Rating;
            entity.Title = movie.Title;
            entity.Description = movie.Description;
            entity.Director = movie.Director;
            entity.ImageUrl = movie.ImageUrl;
            entity.GenreId = movie.GenreId;

            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await context.Movies
                .AnyAsync(m => m.Id == id);
        }

        public async Task<MovieDetailsViewModel> MovieDetailsById(Guid id)
        {
            return await context.Movies
                .Where(b => b.Id == id)
                .Include(b => b.Comments)
                .Select(b => new MovieDetailsViewModel()
                {
                    Id = b.Id,
                    Genre = b.Genre.Name,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Title = b.Title,
                    Rating = b.Rating,
                    Director = b.Director,
                    Comments = b.Comments
                    .Select(c => new CommentViewModel()
                    {
                        Description = c.Description,
                        Id = c.Id
                    }).ToList()
                })
                .FirstAsync();
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

        public async Task<EditMovieViewModel> GetForEditAsync(Guid movieId)
        {
            var movie = await context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                throw new ArgumentException("Movie not found");
            }

            var model = new EditMovieViewModel()
            {
                Id = movieId,
                Director = movie.Director,
                Description = movie.Description,
                Rating = movie.Rating,
                ImageUrl = movie.ImageUrl,
                GenreId = movie.GenreId,
                Title = movie.Title
            };

            model.Genres = await GetGenresAsync();

            return model;

        }

        public async Task<IEnumerable<MovieGenre>> GetGenresAsync()
        {
            return await context.MoviesGenres.ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetMineAsync(string userId)
        {
            var user = await context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UsersMovies)
            .ThenInclude(um => um.Movie)
            .ThenInclude(m => m.Genre)
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            return user.UsersMovies
                .Select(m => new MovieViewModel()
                {
                    Director = m.Movie.Director,
                    Genre = m.Movie.Genre?.Name,
                    Id = m.MovieId,
                    ImageUrl = m.Movie.ImageUrl,
                    Rating = m.Movie.Rating,
                    Title = m.Movie.Title,
                    Description = m.Movie.Description
                });
        }

        public async Task RemoveMovieFromCollectionAsync(Guid movieId, string userId)
        {
            var user = await context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UsersMovies)
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                user.UsersMovies.Remove(movie);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddComment(Guid movieId, string comment)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException("Invalid movie Id");
            }

            if (String.IsNullOrEmpty(comment))
            {
                throw new ArgumentException("Invalid comment");
            }

            var currComment = new Comment()
            {
                Description = comment,
                MovieId = movieId,
                Movie = movie
            };

            movie.Comments.Add(currComment);
            await context.Comments.AddAsync(currComment);
            await context.SaveChangesAsync();
        }
    }
}
