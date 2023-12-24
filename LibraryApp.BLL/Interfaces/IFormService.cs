using LibraryApp.BLL.DTOs;
using LibraryApp.DAL.Entities;

namespace LibraryApp.BLL.Interfaces
{
    public interface IFormService
    {
        Task AddFormAsync(FormDTO formDTO);
        Task DeleteFormAsync(int id);
        Task<IEnumerable<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int id);
        Task UpdateFormAsync(int id, FormDTO formDTO);
    }
}