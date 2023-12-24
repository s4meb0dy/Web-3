using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using LibraryApp.DAL.Persistence.Repositories;

namespace LibraryApp.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext context;

        private IRepository<Book> bookRepository;
        private IRepository<Form> formRepository;
        private IRepository<Reader> readerRepository;

        public IRepository<Book> BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(this.context);
                }

                return bookRepository;
            }
        }
        public IRepository<Form> FormRepository
        {
            get
            {
                if (formRepository == null)
                {
                    formRepository = new FormRepository(this.context);
                }

                return formRepository;
            }
        }

        public IRepository<Reader> ReaderRepository
        {
            get
            {
                if (readerRepository == null)
                {
                    readerRepository = new ReaderRepository(this.context);
                }

                return readerRepository;
            }
        }

        public UnitOfWork(LibraryContext context)
        {
            this.context = context;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
