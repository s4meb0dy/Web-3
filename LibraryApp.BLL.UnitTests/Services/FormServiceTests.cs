using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Services;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Moq;
using NUnit.Framework;

namespace LibraryApp.BLL.UnitTests.Services
{
    [TestFixture]
    public class FormServiceTests
    {
        [Test]
        public async Task GetAllFormsAsync_ShouldReturnAllForms()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.FormRepository.GetAllAsync())
                .ReturnsAsync(new List<Form>
                {
                    new Form { Id = 1, BorrowDate = DateTime.Now, ReaderId = 1, BookId = 1 },
                    new Form { Id = 2, BorrowDate = DateTime.Now, ReaderId = 2, BookId = 2 }
                });

            var formService = new FormService(unitOfWorkMock.Object);

            // Act
            var result = await formService.GetAllFormsAsync();

            // Assert
            Assert.That(result is not null);
            Assert.That(2 == result.Count());
        }

        [Test]
        public async Task GetFormByIdAsync_ShouldReturnFormWithGivenId()
        {
            // Arrange
            var formId = 1;
            var form = new Form { Id = formId, BorrowDate = DateTime.Now, ReaderId = 1, BookId = 1 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.FormRepository.GetByIdAsync(formId))
                .ReturnsAsync(form);

            var formService = new FormService(unitOfWorkMock.Object);

            // Act
            var result = await formService.GetFormByIdAsync(formId);

            // Assert
            Assert.That(result is not null);
            Assert.That(formId == result.Id);
        }

        [Test]
        public async Task AddFormAsync_ShouldAddFormToRepository()
        {
            // Arrange
            var formDTO = new FormDTO { BorrowDate = DateTime.Now, ReaderId = 1, BookId = 1 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var formRepositoryMock = new Mock<IRepository<Form>>();
            unitOfWorkMock.Setup(u => u.FormRepository).Returns(formRepositoryMock.Object);

            var formService = new FormService(unitOfWorkMock.Object);

            // Act
            await formService.AddFormAsync(formDTO);

            // Assert
            unitOfWorkMock.Verify(u => u.FormRepository.AddAsync(It.IsAny<Form>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
