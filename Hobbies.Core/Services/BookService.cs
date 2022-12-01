using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Book;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hobbies.Core.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext context;

        public BookService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookAsync(AddBookViewModel book)
        {
            var entity = new Book()
            {
                Author = book.Author,
                Title = book.Title,
                GenreId = book.GenreId,
                Rating = book.Rating,
                ImageUrl = book.ImageUrl,
                Description = book.Description
            };

            await context.Books.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(Guid bookId, string userId)
        {
            var user = await context.Users
                            .Where(u => u.Id == userId)
                            .Include(u => u.UsersBooks)
                            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid book Id");
            }

            if (!user.UsersBooks.Any(b => b.BookId == bookId))
            {
                user.UsersBooks.Add(new UserBook()
                {
                    BookId = book.Id,
                    UserId = user.Id,
                    User = user,
                    Book = book
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await context.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null)
            {
                throw new ArgumentException("Invalid book Id");
            }

            context.Books.Remove(book);
            context.SaveChanges();
        }

        public async Task EditAsync(EditBookViewModel book)
        {
            var entity = await context.Books.FindAsync(book.Id);

            entity.Rating = book.Rating;
            entity.Title = book.Title;
            entity.Description = book.Description;
            entity.Author = book.Author;
            entity.ImageUrl = book.ImageUrl;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var entities = await context.Books
                .Include(b => b.Genre)
                .ToListAsync();

            return entities
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Genre = b?.Genre.Name
                });
        }

        public async Task<EditBookViewModel> GetForEditAsync(Guid id)
        {
            var book = await context.Books.FindAsync(id);
            var model = new EditBookViewModel()
            {
                Id = id,
                Author = book.Author,
                Description = book.Description,
                Rating = book.Rating,
                ImageUrl = book.ImageUrl,
                GenreId = book.GenreId,
                Title = book.Title
            };

            model.Genres = await GetGenresAsync();

            return model;
        }

        public async Task<IEnumerable<BookGenre>> GetGenresAsync()
        {
            return await context.BooksGenres.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetMineAsync(string userId)
        {
            var user = await context.Users
              .Where(u => u.Id == userId)
              .Include(u => u.UsersBooks)
              .ThenInclude(ub => ub.Book)
              .ThenInclude(b => b.Genre)
              .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            return user.UsersBooks
                .Select(m => new BookViewModel()
                {
                    Author = m.Book.Author,
                    Genre = m.Book.Genre?.Name,
                    Id = m.BookId,
                    ImageUrl = m.Book.ImageUrl,
                    Rating = m.Book.Rating,
                    Title = m.Book.Title,
                    Description = m.Book.Description
                });
        }

        public async Task RemoveBookFromCollectionAsync(Guid bookId, string userId)
        {
            var user = await context.Users
             .Where(u => u.Id == userId)
             .Include(u => u.UsersBooks)
             .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var book = user.UsersBooks.FirstOrDefault(m => m.BookId == bookId);

            if (book != null)
            {
                user.UsersBooks.Remove(book);
                await context.SaveChangesAsync();
            }
        }
    }
}
