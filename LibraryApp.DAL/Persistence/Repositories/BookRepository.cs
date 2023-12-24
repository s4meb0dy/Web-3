using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Persistence.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
    }
}
