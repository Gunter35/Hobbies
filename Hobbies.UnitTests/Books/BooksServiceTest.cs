﻿using Hobbies.Core.Contracts;
using Hobbies.Core.Models.Book;
using Hobbies.Core.Models.Comment;
using Hobbies.Core.Services;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.UnitTests.Books
{
    public class BooksServiceTest
    {
        [Test]
        public async Task Create_Method_Should_Add_Book_To_The_Db()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await bookService.AddBookAsync(book);

            Assert.AreEqual(1, dbContext.Books.Count());
        }

        [Test]
        public async Task AddCommentShouldCreateCommentAndAddItToBooksComments()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await bookService.AddBookAsync(book);

            string comment = "something to test";

            await bookService.AddComment(dbContext.Books.First().Id, comment);

            Assert.AreEqual(comment, dbContext.Comments.First().Description);

        }

        [Test]
        public async Task CheckingIfDeleteWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await bookService.AddBookAsync(book);
            await bookService.DeleteAsync(dbContext.Books.First().Id);

            Assert.AreEqual(0, dbContext.Books.Count());
        }

        [Test]
        public async Task CheckingIfGetForEditWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await bookService.AddBookAsync(book);

            var result = await bookService.GetForEditAsync(dbContext.Books.First().Id);

            Assert.AreEqual(dbContext.Books.First().Id, result.Id);
        }

        [Test]
        public async Task CheckingIfEditWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await bookService.AddBookAsync(book);

            var result = await bookService.GetForEditAsync(dbContext.Books.First().Id);
            result.Title = "Harry Potter 2";
            await bookService.EditAsync(result);

            Assert.AreNotEqual(book.Title, dbContext.Books.First().Title);
        }

        [Test]
        public async Task CheckingIfGetGenresWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            var genre = new BookGenre()
            {
                Name = "horror",
                Id = Guid.NewGuid()
            };
            var genre2 = new BookGenre()
            {
                Name = "comedy",
                Id = Guid.NewGuid()
            };

            dbContext.BooksGenres.Add(genre);
            dbContext.BooksGenres.Add(genre2);


            var result = await bookService.GetGenresAsync();

            Assert.AreEqual(dbContext.BooksGenres.Count(), result.Count());
        }

        [Test]
        public async Task CheckingIfExistsWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await bookService.AddBookAsync(book);
            var result = await bookService.Exists(dbContext.Books.First().Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckingIfGetAllWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var bookService = new BookService(dbContext);

            Guid guid = Guid.NewGuid();

            var book = new Book()
            {
                Title = "Harry Potter",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            var book2 = new Book()
            {
                Title = "Harry Potter 2",
                Author = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            dbContext.Books.Add(book);
            dbContext.Books.Add(book2);

            var result = await bookService.GetAllAsync();
            
            Assert.AreEqual(dbContext.Books.Count(), result.Count());
        }

        //[Test]
        //public async Task CheckingIfGetAllWorks()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
        //       .UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    var dbContext = new ApplicationDbContext(optionsBuilder.Options);

        //    var movieService = new MovieService(dbContext);
        //}
    }
}
