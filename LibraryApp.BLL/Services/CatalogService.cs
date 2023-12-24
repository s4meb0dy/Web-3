using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork unitOfWork;

        public CatalogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string? title = null, string? author = null, int? year = null)
        {
            var books = await this.unitOfWork.BookRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(title))
                books = books.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(author))
                books = books.Where(b => b.Author.Contains(author));

            if (year is not null)
                books = books.Where(b => b.Year == year);

            return books;
        }
    }
}
