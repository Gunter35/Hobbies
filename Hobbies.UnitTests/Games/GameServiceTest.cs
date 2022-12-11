using Hobbies.Core.Models.Game;
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

namespace Hobbies.UnitTests.Games
{
    public class GameServiceTest
    {
        [Test]
        public async Task Create_Method_Should_Add_Game_To_The_Db()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new AddGameViewModel()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await gameService.AddGameAsync(game);

            Assert.AreEqual(1, dbContext.Games.Count());
        }

        [Test]
        public async Task AddCommentShouldCreateCommentAndAddItToGamesComments()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new AddGameViewModel()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await gameService.AddGameAsync(game);

            string comment = "something to test";

            await gameService.AddComment(dbContext.Games.First().Id, comment);

            Assert.AreEqual(comment, dbContext.Comments.First().Description);

        }

        [Test]
        public async Task CheckingIfDeleteWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new AddGameViewModel()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await gameService.AddGameAsync(game);
            await gameService.DeleteAsync(dbContext.Games.First().Id);

            Assert.AreEqual(0, dbContext.Games.Count());
        }

        [Test]
        public async Task CheckingIfGetForEditWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new AddGameViewModel()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await gameService.AddGameAsync(game);

            var result = await gameService.GetForEditAsync(dbContext.Games.First().Id);

            Assert.AreEqual(dbContext.Games.First().Id, result.Id);
        }

        [Test]
        public async Task CheckingIfEditWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new AddGameViewModel()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await gameService.AddGameAsync(game);

            var result = await gameService.GetForEditAsync(dbContext.Games.First().Id);
            result.Name = "Harry Potter 2";
            await gameService.EditAsync(result);

            Assert.AreNotEqual(game.Name, dbContext.Games.First().Name);
        }
        [Test]
        public async Task CheckingIfGetGenresWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            var genre = new GameGenre()
            {
                Name = "horror",
                Id = Guid.NewGuid()
            };
            var genre2 = new GameGenre()
            {
                Name = "comedy",
                Id = Guid.NewGuid()
            };

            dbContext.GamesGenres.Add(genre);
            dbContext.GamesGenres.Add(genre2);


            var result = await gameService.GetGenresAsync();

            Assert.AreEqual(dbContext.GamesGenres.Count(), result.Count());
        }

        [Test]
        public async Task CheckingIfExistsWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new AddGameViewModel()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            await gameService.AddGameAsync(game);
            var result = await gameService.Exists(dbContext.Games.First().Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckingIfGetAllWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);

            Guid guid = Guid.NewGuid();

            var game = new Game()
            {
                Name = "Harry Potter",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            var game2 = new Game()
            {
                Name = "Harry Potter 2",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = guid,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            dbContext.Games.Add(game);
            dbContext.Games.Add(game2);

            var result = await gameService.GetAllAsync();

            Assert.AreEqual(dbContext.Games.Count(), result.Count());
        }

        [Test]
        public async Task CheckingIfAddGameToCollectionWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);
            Guid genreId = Guid.NewGuid();
            Guid gameId = Guid.NewGuid();


            var user = new User()
            {
                Id = "1",
                UserName = "Pesho",
                Email = "pesho@gmail.com"
            };

            var game = new Game()
            {
                Id = gameId,
                Name = "Harry Potter 2",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = genreId,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            await dbContext.AddAsync(game);
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            await gameService.AddGameToCollectionAsync(gameId, "1");

            Assert.AreEqual(1, dbContext.Users.First().UsersGames.Count());
            Assert.AreEqual(1, dbContext.UsersGames.Count());
            Assert.AreEqual("1", dbContext.UsersGames.First().UserId);
            Assert.AreEqual(gameId, dbContext.UsersGames.First().GameId);
        }

        [Test]
        public async Task CheckingIfGetMineWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);
            Guid genreId = Guid.NewGuid();
            Guid gameId = Guid.NewGuid();

            var user = new User()
            {
                Id = "1",
                UserName = "Pesho",
                Email = "pesho@gmail.com"
            };

            var game = new Game()
            {
                Id = gameId,
                Name = "Harry Potter 2",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = genreId,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            await dbContext.AddAsync(game);
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            await gameService.AddGameToCollectionAsync(gameId, "1");

            var result = await gameService.GetMineAsync("1");

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task CheckingIfRemoveGameFromCollectionWorks()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);
            Guid genreId = Guid.NewGuid();
            Guid gameId = Guid.NewGuid();

            var user = new User()
            {
                Id = "1",
                UserName = "Pesho",
                Email = "pesho@gmail.com"
            };

            var game = new Game()
            {
                Id = gameId,
                Name = "Harry Potter 2",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = genreId,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            await dbContext.AddAsync(game);
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            await gameService.AddGameToCollectionAsync(gameId, "1");

            Assert.AreEqual(1, dbContext.UsersGames.Count());

            await gameService.RemoveGameFromCollectionAsync(gameId, "1");

            Assert.AreEqual(0, dbContext.UsersGames.Count());
        }

        [Test]
        public async Task CheckingGameDetailsWorks()
        {

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var gameService = new GameService(dbContext);
            Guid genreId = Guid.NewGuid();
            Guid gameId = Guid.NewGuid();

            var genre = new GameGenre()
            {
                Id = genreId,
                Name = "horror"
            };

            var game = new Game()
            {
                Id = gameId,
                Name = "Harry Potter 2",
                Creator = "J. K. Rowling",
                ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                Rating = 10,
                GenreId = genreId,
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Genre = genre
            };

            await dbContext.AddAsync(genre);
            await dbContext.AddAsync(game);
            await dbContext.SaveChangesAsync();

            var result = gameService.GameDetailsById(gameId);

            Assert.IsNotNull(result);
            Assert.AreEqual(game.Name, result.Result.Name);
            Assert.AreEqual(game.Id, result.Result.Id);
        }
    }
}
