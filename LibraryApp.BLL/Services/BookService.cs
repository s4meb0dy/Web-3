using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await unitOfWork.BookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await unitOfWork.BookRepository.GetByIdAsync(id);
        }

        public async Task AddBookAsync(BookDTO bookDTO)
        {
            var book = new Book
            {
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                Year = bookDTO.Year,
                IsAvailable = bookDTO.IsAvailable
            };

            await unitOfWork.BookRepository.AddAsync(book);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(int id, BookDTO bookDTO)
        {
            var existingBook = await unitOfWork.BookRepository.GetByIdAsync(id);

            if (existingBook != null)
            {
                existingBook.Title = bookDTO.Title;
                existingBook.Author = bookDTO.Author;
                existingBook.Year = bookDTO.Year;
                existingBook.IsAvailable = bookDTO.IsAvailable;

                await unitOfWork.BookRepository.UpdateAsync(existingBook);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            await unitOfWork.BookRepository.DeleteAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }

}
