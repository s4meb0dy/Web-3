using LibraryApp.BLL.DTOs;
using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Interfaces
{
    public interface IReaderService
    {
        Task AddReaderAsync(ReaderDTO readerDTO);
        Task DeleteReaderAsync(int id);
        Task<IEnumerable<Reader>> GetAllReadersAsync();
        Task<Reader> GetReaderByIdAsync(int id);
        Task UpdateReaderAsync(int id, ReaderDTO readerDTO);
    }
}