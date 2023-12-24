using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL.Persistence.Repositories
{
    public class FormRepository : IRepository<Form>
    {
        private readonly LibraryContext _context;

        public FormRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            return await _context.Forms.Include(x => x.Reader).Include(x => x.Book).ToListAsync();
        }

        public async Task<Form> GetByIdAsync(int id)
        {
            return await _context.Forms.FindAsync(id);
        }

        public async Task AddAsync(Form form)
        {
            _context.Forms.Add(form);
        }

        public async Task UpdateAsync(Form form)
        {
            _context.Entry(form).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form != null)
            {
                _context.Forms.Remove(form);
            }
        }
    }

}
