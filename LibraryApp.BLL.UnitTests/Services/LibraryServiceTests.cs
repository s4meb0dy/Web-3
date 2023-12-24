using LibraryApp.BLL.Exceptions;
using LibraryApp.BLL.Services;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Moq;
using NUnit.Framework;

namespace LibraryApp.BLL.UnitTests.Services
{
    [TestFixture]
    public class LibraryServiceTests
    {
        [Test]
        public async Task BorrowBooksAsync_ShouldBorrowBooksSuccessfully()
        {
            // Arrange
            var readerId = 1;
            var bookIds = new List<int> { 1, 2, 3 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var readerRepositoryMock = new Mock<IRepository<Reader>>();
            unitOfWorkMock.Setup(u => u.ReaderRepository).Returns(readerRepositoryMock.Object);

            var bookRepositoryMock = new Mock<IRepository<Book>>();
            unitOfWorkMock.Setup(u => u.BookRepository).Returns(bookRepositoryMock.Object);

            var formRepositoryMock = new Mock<IRepository<Form>>();
            unitOfWorkMock.Setup(u => u.FormRepository).Returns(formRepositoryMock.Object);

            var existingReader = new Reader { Id = readerId, FullName = "John Doe", Address = "123 Main St" };
            unitOfWorkMock.Setup(u => u.ReaderRepository.GetByIdAsync(readerId)).ReturnsAsync(existingReader);

            var availableBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", IsAvailable = true },
                new Book { Id = 2, Title = "Book2", IsAvailable = true },
                new Book { Id = 3, Title = "Book3", IsAvailable = true }
            };
            unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync()).ReturnsAsync(availableBooks);

            var libraryService = new LibraryService(unitOfWorkMock.Object);

            // Act
            await libraryService.BorrowBooksAsync(readerId, bookIds);

            // Assert
            unitOfWorkMock.Verify(u => u.BookRepository.UpdateAsync(It.IsAny<Book>()), Times.Exactly(3));
            unitOfWorkMock.Verify(u => u.FormRepository.AddAsync(It.IsAny<Form>()), Times.Exactly(3));
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void BorrowBooksAsync_ShouldThrowException_WhenReaderNotFound()
        {
            // Arrange
            var readerId = 1;
            var bookIds = new List<int> { 1, 2, 3 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.ReaderRepository.GetByIdAsync(readerId)).ReturnsAsync((Reader)null);

            var libraryService = new LibraryService(unitOfWorkMock.Object);

            // Act, Assert
            Assert.ThrowsAsync<ReaderNotFoundException>(() => libraryService.BorrowBooksAsync(readerId, bookIds));
        }

        [Test]
        public void BorrowBooksAsync_ShouldThrowException_WhenBooksUnavailable()
        {
            // Arrange
            var readerId = 1;
            var bookIds = new List<int> { 1, 2, 3 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var existingReader = new Reader { Id = readerId, FullName = "John Doe", Address = "123 Main St" };
            unitOfWorkMock.Setup(u => u.ReaderRepository.GetByIdAsync(readerId)).ReturnsAsync(existingReader);

            var unavailableBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Book1", IsAvailable = false },
                new Book { Id = 2, Title = "Book2", IsAvailable = false },
                new Book { Id = 3, Title = "Book3", IsAvailable = false }
            };
            unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync()).ReturnsAsync(unavailableBooks);

            var libraryService = new LibraryService(unitOfWorkMock.Object);

            // Act, Assert
            Assert.ThrowsAsync<BooksUnavailableException>(() => libraryService.BorrowBooksAsync(readerId, bookIds));
        }
    }
}
