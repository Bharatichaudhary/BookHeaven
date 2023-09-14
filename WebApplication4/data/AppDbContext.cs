using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<BookModel> Book { get; set; }
        public DbSet<UserModel> User { get; set; }

    }
}
