using LibraryApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DAL
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Reader> Readers { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        { 
        }
    }
}
