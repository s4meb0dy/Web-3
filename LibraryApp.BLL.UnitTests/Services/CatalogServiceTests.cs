using LibraryApp.BLL.Services;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Moq;
using NUnit.Framework;

namespace LibraryApp.BLL.UnitTests.Services
{
    [TestFixture]
    public class CatalogServiceTests
    {
        [Test]
        public async Task SearchBooksAsync_ShouldReturnMatchingBooks()
        {
            // Arrange
            var title = "Book";
            var author = "Author";
            var year = 2022;

            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", Author = "Author1", Year = 2022 },
                new Book { Id = 2, Title = "Book2", Author = "Author2", Year = 2022 },
                new Book { Id = 3, Title = "Book3", Author = "Author3", Year = 2023 }
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync()).ReturnsAsync(books);

            var catalogService = new CatalogService(unitOfWorkMock.Object);

            // Act
            var result = await catalogService.SearchBooksAsync(title, author, year);

            // Assert
            Assert.That(result is not null);
            Assert.That(2 == result.Count()); // Two books match the criteria
        }

        [Test]
        public async Task SearchBooksAsync_ShouldReturnAllBooks_WhenNoFilterProvided()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", Author = "Author1", Year = 2022 },
                new Book { Id = 2, Title = "Book2", Author = "Author2", Year = 2022 },
                new Book { Id = 3, Title = "Book3", Author = "Author3", Year = 2023 }
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync()).ReturnsAsync(books);

            var catalogService = new CatalogService(unitOfWorkMock.Object);

            // Act
            var result = await catalogService.SearchBooksAsync();

            // Assert
            Assert.That(result is not null);
            Assert.That(3 == result.Count()); // All books are returned
        }

        // Additional tests can be written for various search scenarios
    }
}
