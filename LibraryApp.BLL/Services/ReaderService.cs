using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReaderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Reader>> GetAllReadersAsync()
        {
            return await unitOfWork.ReaderRepository.GetAllAsync();
        }

        public async Task<Reader> GetReaderByIdAsync(int id)
        {
            return await unitOfWork.ReaderRepository.GetByIdAsync(id);
        }

        public async Task AddReaderAsync(ReaderDTO readerDTO)
        {
            var reader = new Reader
            {
                FullName = readerDTO.FullName,
                Address = readerDTO.Address
            };

            await unitOfWork.ReaderRepository.AddAsync(reader);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateReaderAsync(int id, ReaderDTO readerDTO)
        {
            var existingReader = await unitOfWork.ReaderRepository.GetByIdAsync(id);

            if (existingReader != null)
            {
                existingReader.FullName = readerDTO.FullName;
                existingReader.Address = readerDTO.Address;

                await unitOfWork.ReaderRepository.UpdateAsync(existingReader);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteReaderAsync(int id)
        {
            await unitOfWork.ReaderRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }

}
