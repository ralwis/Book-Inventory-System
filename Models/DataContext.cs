using Microsoft.EntityFrameworkCore;

namespace Book_Inventory_System.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Shelve> Shelves { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
