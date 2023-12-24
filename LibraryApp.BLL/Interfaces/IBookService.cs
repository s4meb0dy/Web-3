using LibraryApp.BLL.DTOs;
using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(BookDTO bookDTO);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task UpdateBookAsync(int id, BookDTO bookDTO);
    }
}