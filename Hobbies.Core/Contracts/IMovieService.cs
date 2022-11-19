using Hobbies.Core.Models.Movie;
using Hobbies.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.Core.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllAsync();

        Task<IEnumerable<MovieGenre>> GetGenresAsync();

        Task AddMovieAsync(AddMovieViewModel movie);

        Task AddMovieToCollectionAsync(Guid movieId, string userId);


    }
}
