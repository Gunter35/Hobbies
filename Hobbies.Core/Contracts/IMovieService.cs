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

        Task<IEnumerable<MovieViewModel>> GetMineAsync(string userId);

        Task RemoveMovieFromCollectionAsync(Guid movieId, string userId);

        Task<EditMovieViewModel> GetForEditAsync(Guid id);

        Task EditAsync(EditMovieViewModel movie);

        Task DeleteAsync(Guid id);

        Task<bool> Exists(Guid id);

        Task<MovieDetailsViewModel> MovieDetailsById(Guid id);

    }
}
