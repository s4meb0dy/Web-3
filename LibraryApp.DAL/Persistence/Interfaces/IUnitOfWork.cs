using LibraryApp.DAL.Entities;

namespace LibraryApp.DAL.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> BookRepository { get; }
        IRepository<Form> FormRepository { get; }
        IRepository<Reader> ReaderRepository { get; }

        Task SaveChangesAsync();
    }

}
