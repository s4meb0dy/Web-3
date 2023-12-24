using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<Book>> SearchBooksAsync(string? title = null, string? author = null, int? year = null);
    }
}