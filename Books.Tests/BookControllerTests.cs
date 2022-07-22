using Books.Data;
using Books.Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using Books.WebAPI.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Books.Tests
{
    public class BookControllerTests
    {
        private async Task SeedData(BooksContext context)
        {
            context.Books.AddRange(
                new Book { Title = "My First Book" },
                new Book { Title = "My Second Book", NumberOfCopies = 42 }
                );
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task Controller_ListBooks_ReturnsEmpty_IfNoBooksAreStored()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_ListBooks_ReturnsEmpty_IfNoBooksAreStored))
                .Options;
            using var booksDbContext = new BooksContext(options);

            var sut = new BooksController(booksDbContext);

            var result = await sut.Get();

            Assert.That(result.Count(), Is.Zero);
        }

        [Test]
        public async Task Controller_ListBooks_ReturnsAllStoredBooks()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_ListBooks_ReturnsAllStoredBooks))
                .Options;
            using var booksDbContext = new BooksContext(options);
            await SeedData(booksDbContext);

            var sut = new BooksController(booksDbContext);

            var result = await sut.Get();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("My First Book"));
            Assert.That(result[0].NumberOfCopies, Is.Null);
            Assert.That(result[1].Title, Is.EqualTo("My Second Book"));
            Assert.That(result[1].NumberOfCopies, Is.EqualTo(42));
        }

        [Test]
        public async Task Controller_GetBook_ReturnsBookById()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_GetBook_ReturnsBookById))
                .Options;
            using var booksDbContext = new BooksContext(options);
            await SeedData(booksDbContext);

            var sut = new BooksController(booksDbContext);

            var testId = 1;

            var result = await sut.Get(testId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("My First Book"));
        }

        [Test]
        public async Task Controller_GetBook_ReturnsNull_IfNotFound()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_GetBook_ReturnsNull_IfNotFound))
                .Options;
            using var booksDbContext = new BooksContext(options);
            await SeedData(booksDbContext);

            var sut = new BooksController(booksDbContext);

            var testId = 42;

            var result = await sut.Get(testId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Controller_PostBook_CreatesABookInTheDatabase()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_PostBook_CreatesABookInTheDatabase))
                .Options;
            using var booksDbContext = new BooksContext(options);

            Assert.That(booksDbContext.Books.Count(), Is.Zero);

            var sut = new BooksController(booksDbContext);

            var testTitle = "My favourite book";
            var bookToAdd = new Book { Title = testTitle };

            await sut.Post(bookToAdd);

            var addedBook = booksDbContext.Books.Single();
            Assert.That(addedBook.Title, Is.EqualTo(testTitle));
        }

        [Test]
        public async Task Controller_PutBook_ReturnsNotFound_IfIdNotInStore()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_PutBook_ReturnsNotFound_IfIdNotInStore))
                .Options;
            using var booksDbContext = new BooksContext(options);

            var sut = new BooksController(booksDbContext);

            var testId = 42;
            var newTitle = "Title was edited";

            var result = await sut.Put(testId, new Book { Title = newTitle });

            Assert.That(result is NotFoundObjectResult);
            Assert.That(((NotFoundObjectResult)result).Value, Is.EqualTo("Book with id 42 is not found"));
        }


        [Test]
        public async Task Controller_PutBook_CanUpdateTitle()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_PutBook_CanUpdateTitle))
                .Options;
            using var booksDbContext = new BooksContext(options);
            await SeedData(booksDbContext);

            var sut = new BooksController(booksDbContext);

            var testId = 1;
            var newTitle = "Title was edited";

            await sut.Put(testId, new Book { Title = newTitle });

            var editedBook = booksDbContext.Books.Find(testId);
            Assert.That(editedBook.Title, Is.EqualTo(newTitle));
        }

        [Test]
        public async Task Controller_DeleteBook_ReturnsNotFound_IfIdNotInStore()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_DeleteBook_ReturnsNotFound_IfIdNotInStore))
                .Options;
            using var booksDbContext = new BooksContext(options);

            var sut = new BooksController(booksDbContext);

            var testId = 42;

            var result = await sut.Delete(testId);

            Assert.That(result is NotFoundObjectResult);
            Assert.That(((NotFoundObjectResult)result).Value, Is.EqualTo("Book with id 42 is not found"));
        }

        [Test]
        public async Task Controller_DeleteBook_RemovesFromDatastore()
        {
            var options = new DbContextOptionsBuilder<BooksContext>()
                .UseInMemoryDatabase(nameof(Controller_DeleteBook_RemovesFromDatastore))
                .Options;
            using var booksDbContext = new BooksContext(options);
            await SeedData(booksDbContext);

            var sut = new BooksController(booksDbContext);

            var testId = 1;
            var bookToDelete = booksDbContext.Books.Find(testId);
            Assert.That(bookToDelete, Is.Not.Null);

            await sut.Delete(testId);

            var deletedBook = booksDbContext.Books.Find(testId);
            Assert.That(deletedBook, Is.Null);
        }
    }
}