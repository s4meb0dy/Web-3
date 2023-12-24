using LibraryApp.BLL.Exceptions;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;

namespace LibraryApp.BLL.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IUnitOfWork unitOfWork;

        public LibraryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task BorrowBooksAsync(int readerId, IEnumerable<int> bookIds)
        {
            var reader = this.unitOfWork.ReaderRepository.GetByIdAsync(readerId).Result;

            if (reader is null)
                throw new ReaderNotFoundException(readerId);

            var booksToBorrow = this.unitOfWork.BookRepository.GetAllAsync().Result
                .Where(b => bookIds.Contains(b.Id) && b.IsAvailable)
                .Take(10)
                .ToList();

            if (booksToBorrow.Count == 0)
                throw new BooksUnavailableException(bookIds);

            foreach (var book in booksToBorrow)
            {
                book.IsAvailable = false;

                var form = new Form
                {
                    BorrowDate = DateTime.Now,
                    ReaderId = readerId,
                    BookId = book.Id
                };

                await this.unitOfWork.FormRepository.AddAsync(form);
                await this.unitOfWork.BookRepository.UpdateAsync(book);
            }

            await this.unitOfWork.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int bookId)
        {
            var book = await this.unitOfWork.BookRepository.GetByIdAsync(bookId);

            if (book is null)
                throw new ArgumentException("Book doesn't exist");

            if (!book.IsAvailable)
            {
                book.IsAvailable = true;
                var forms = await this.unitOfWork.FormRepository.GetAllAsync();

                var idsToDelete = forms.Where(x => x.BookId == bookId).Select(x => x.Id).ToList();
                foreach (var id in idsToDelete)
                {
                    await this.unitOfWork.FormRepository.DeleteAsync(id);
                }

                await this.unitOfWork.BookRepository.UpdateAsync(book);
                await this.unitOfWork.SaveChangesAsync();
            }
        }
    }
}
