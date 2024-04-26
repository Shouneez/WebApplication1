using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.Database
{
    public class LibraryApiDbContext : DbContext
    {
        public LibraryApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public  DbSet <Book> Book  { get; set; } 
        public  DbSet <User> User  { get; set; }
        public DbSet<BorrowBook> BorrowBook { get; set; }

    }
}
