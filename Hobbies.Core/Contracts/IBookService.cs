using Hobbies.Core.Models.Book;
using Hobbies.Infrastructure.Data.Models;

namespace Hobbies.Core.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<BookGenre>> GetGenresAsync();

        Task AddBookAsync(AddBookViewModel book);

        Task AddBookToCollectionAsync(Guid bookId, string userId);

        Task<IEnumerable<BookViewModel>> GetMineAsync(string userId);

        Task RemoveBookFromCollectionAsync(Guid bookId, string userId);
    }
}
