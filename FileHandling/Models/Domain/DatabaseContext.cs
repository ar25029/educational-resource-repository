using Microsoft.EntityFrameworkCore;

namespace FileHandling.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
               
        }

        public DbSet<Product> Products { get; set; }
    }
}
