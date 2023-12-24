using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Persistence.Repositories
{
    public class ReaderRepository : IRepository<Reader>
    {
        private readonly LibraryContext _context;

        public ReaderRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reader>> GetAllAsync()
        {
            return await _context.Readers.ToListAsync();
        }

        public async Task<Reader> GetByIdAsync(int id)
        {
            return await _context.Readers.FindAsync(id);
        }

        public async Task AddAsync(Reader reader)
        {
            _context.Readers.Add(reader);
        }

        public async Task UpdateAsync(Reader reader)
        {
            _context.Entry(reader).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader != null)
            {
                _context.Readers.Remove(reader);
            }
        }
    }

}
