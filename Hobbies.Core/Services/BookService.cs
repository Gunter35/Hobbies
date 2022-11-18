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
                GenreId = book.CategoryId,
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

            var book = await context.Books.FirstOrDefaultAsync(m => m.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid book Id");
            }

            if (!book.UsersBooks.Any(m => m.BookId == bookId))
            {
                book.UsersBooks.Add(new UserBook()
                {
                    BookId = book.Id,
                    UserId = user.Id,
                    User = user,
                    Book = book
                });
            }

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

        public async Task<IEnumerable<BookGenre>> GetGenresAsync()
        {
            return await context.BooksGenres.ToListAsync();
        }
    }
}
