using Hobbies.Core.Models.Book;
using Hobbies.Core.Models.Comment;
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

        Task<EditBookViewModel> GetForEditAsync(Guid gameId);

        Task EditAsync(EditBookViewModel book);

        Task DeleteAsync(Guid id);

        Task<bool> Exists(Guid id);

        Task<BookDetailsViewModel> BookDetailsById(Guid id);

        Task AddComment(Guid bookId, string comment);
    }
}
