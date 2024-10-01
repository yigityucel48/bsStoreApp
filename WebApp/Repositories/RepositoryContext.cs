using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options):
            base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
