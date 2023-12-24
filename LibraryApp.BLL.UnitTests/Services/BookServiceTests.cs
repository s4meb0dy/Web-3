using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Services;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Moq;
using NUnit.Framework;

namespace LibraryApp.BLL.UnitTests.Services
{
    [TestFixture]
    public class BookServiceTests
    {
        [Test]
        public async Task GetAllBooksAsync_ShouldReturnAllBooks()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync())
                .ReturnsAsync(new List<Book>
                {
                    new Book { Id = 1, Title = "Book1", Author = "Author1", Year = 2022, IsAvailable = true },
                    new Book { Id = 2, Title = "Book2", Author = "Author2", Year = 2022, IsAvailable = true },
                    new Book { Id = 3, Title = "Book3", Author = "Author3", Year = 2023, IsAvailable = false }
                });

            var bookService = new BookService(unitOfWorkMock.Object);

            // Act
            var result = await bookService.GetAllBooksAsync();

            // Assert
            Assert.That(result is not null);
            Assert.That(3 == result.Count());
        }

        [Test]
        public async Task GetBookByIdAsync_ShouldReturnBookWithGivenId()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Title = "Book1", Author = "Author1", Year = 2022, IsAvailable = true };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.BookRepository.GetByIdAsync(bookId))
                .ReturnsAsync(book);

            var bookService = new BookService(unitOfWorkMock.Object);

            // Act
            var result = await bookService.GetBookByIdAsync(bookId);

            // Assert
            Assert.That(result is not null);
            Assert.That(bookId == result.Id);
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBookToRepository()
        {
            // Arrange
            var bookDTO = new BookDTO { Title = "NewBook", Author = "NewAuthor", Year = 2023, IsAvailable = true };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var bookRepositoryMock = new Mock<IRepository<Book>>();
            unitOfWorkMock.Setup(u => u.BookRepository).Returns(bookRepositoryMock.Object);

            var bookService = new BookService(unitOfWorkMock.Object);

            // Act
            await bookService.AddBookAsync(bookDTO);

            // Assert
            unitOfWorkMock.Verify(u => u.BookRepository.AddAsync(It.IsAny<Book>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        // Similar tests can be written for UpdateBookAsync, DeleteBookAsync, and other scenarios
    }
}
