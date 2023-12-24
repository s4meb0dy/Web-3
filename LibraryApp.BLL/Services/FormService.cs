using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork unitOfWork;

        public FormService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Form>> GetAllFormsAsync()
        {
            return await unitOfWork.FormRepository.GetAllAsync();
        }

        public async Task<Form> GetFormByIdAsync(int id)
        {
            return await unitOfWork.FormRepository.GetByIdAsync(id);
        }

        public async Task AddFormAsync(FormDTO formDTO)
        {
            var form = new Form
            {
                BorrowDate = formDTO.BorrowDate,
                ReaderId = formDTO.ReaderId,
                BookId = formDTO.BookId
            };

            await unitOfWork.FormRepository.AddAsync(form);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateFormAsync(int id, FormDTO formDTO)
        {
            var existingForm = await unitOfWork.FormRepository.GetByIdAsync(id);

            if (existingForm != null)
            {
                existingForm.BorrowDate = formDTO.BorrowDate;
                existingForm.ReaderId = formDTO.ReaderId;
                existingForm.BookId = formDTO.BookId;

                await unitOfWork.FormRepository.UpdateAsync(existingForm);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteFormAsync(int id)
        {
            await unitOfWork.FormRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }

}
