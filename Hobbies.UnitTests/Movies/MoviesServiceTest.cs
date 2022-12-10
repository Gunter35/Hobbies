using Hobbies.Core.Models.Movie;
using Hobbies.Core.Services;
using Hobbies.Infrastructure.Data;
using Hobbies.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobbies.UnitTests.Movies
{
    public class MoviesServiceTest
    {
        [Test]
        public async Task Create_Method_Should_Add_Movie_To_The_Db()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new AddMovieViewModel()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await movieService.AddMovieAsync(movie);

            Assert.AreEqual(1, dbContext.Movies.Count());
        }

        [Test]
        public async Task AddCommentShouldCreateCommentAndAddItToMoviesComments()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new AddMovieViewModel()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await movieService.AddMovieAsync(movie);

            string comment = "something to test";

            await movieService.AddComment(dbContext.Movies.First().Id, comment);

            Assert.AreEqual(comment, dbContext.Comments.First().Description);

        }

        [Test]
        public async Task CheckingIfDeleteWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new AddMovieViewModel()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await movieService.AddMovieAsync(movie);
            await movieService.DeleteAsync(dbContext.Movies.First().Id);

            Assert.AreEqual(0, dbContext.Movies.Count());
        }

        [Test]
        public async Task CheckingIfGetForEditWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "HobbiesTest");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
             
            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new AddMovieViewModel()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await movieService.AddMovieAsync(movie);

            var result = await movieService.GetForEditAsync(dbContext.Movies.First().Id);

            Assert.AreEqual(dbContext.Movies.First().Id, result.Id);
        }

        [Test]
        public async Task CheckingIfEditWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new AddMovieViewModel()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await movieService.AddMovieAsync(movie);

            var result = await movieService.GetForEditAsync(dbContext.Movies.First().Id);
            result.Title = "Harry Potter 2";
            await movieService.EditAsync(result);

            Assert.AreNotEqual(movie.Title, dbContext.Movies.First().Title);
        }

        [Test]
        public async Task CheckingIfGetGenresWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            var genre = new MovieGenre()
            {
                Name = "horror",
                Id = Guid.NewGuid()
            };
            var genre2 = new MovieGenre()
            {
                Name = "comedy",
                Id = Guid.NewGuid()
            };

            dbContext.MoviesGenres.Add(genre);
            dbContext.MoviesGenres.Add(genre2);


            var result = await movieService.GetGenresAsync();

            Assert.AreEqual(dbContext.MoviesGenres.Count(), result.Count());
        }

        [Test]
        public async Task CheckingIfExistsWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new AddMovieViewModel()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await movieService.AddMovieAsync(movie);
            var result = await movieService.Exists(dbContext.Movies.First().Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckingIfGetAllWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var movieService = new MovieService(dbContext);

            Guid guid = Guid.NewGuid();

            var movie = new Movie()
            {
                Title = "Harry Potter",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            var movie2 = new Movie()
            {
                Title = "Harry Potter 2",
                Director = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            dbContext.Movies.Add(movie);
            dbContext.Movies.Add(movie2);

            var result = await movieService.GetAllAsync();

            Assert.AreEqual(dbContext.Movies.Count(), result.Count());
        }

        
    }
}
