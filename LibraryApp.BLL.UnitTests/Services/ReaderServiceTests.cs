using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Services;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using LibraryApp.DAL.Persistence.Repositories;
using Moq;
using NUnit.Framework;

namespace LibraryApp.BLL.UnitTests.Services
{
    [TestFixture]
    public class ReaderServiceTests
    {
        [Test]
        public async Task GetAllReadersAsync_ShouldReturnAllReaders()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.ReaderRepository.GetAllAsync())
                .ReturnsAsync(new List<Reader>
                {
                    new Reader { Id = 1, FullName = "John Doe", Address = "123 Main St" },
                    new Reader { Id = 2, FullName = "Jane Doe", Address = "456 Oak St" }
                });

            var readerService = new ReaderService(unitOfWorkMock.Object);

            // Act
            var result = await readerService.GetAllReadersAsync();

            // Assert
            Assert.That(result is not null);
            Assert.That(2 == result.Count());
        }

        [Test]
        public async Task GetReaderByIdAsync_ShouldReturnReaderWithGivenId()
        {
            // Arrange
            var readerId = 1;
            var reader = new Reader { Id = readerId, FullName = "John Doe", Address = "123 Main St" };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.ReaderRepository.GetByIdAsync(readerId))
                .ReturnsAsync(reader);

            var readerService = new ReaderService(unitOfWorkMock.Object);

            // Act
            var result = await readerService.GetReaderByIdAsync(readerId);

            // Assert
            Assert.That(result is not null);
            Assert.That(readerId == result.Id);
            Assert.That("John Doe" == result.FullName);
        }

        [Test]
        public async Task AddReaderAsync_ShouldAddReaderToRepository()
        {
            // Arrange
            var readerDTO = new ReaderDTO { FullName = "John Doe", Address = "123 Main St" };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var readerRepositoryMock = new Mock<IRepository<Reader>>();
            unitOfWorkMock.Setup(u => u.ReaderRepository).Returns(readerRepositoryMock.Object);
            var readerService = new ReaderService(unitOfWorkMock.Object);

            // Act
            await readerService.AddReaderAsync(readerDTO);

            // Assert
            unitOfWorkMock.Verify(u => u.ReaderRepository.AddAsync(It.IsAny<Reader>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
