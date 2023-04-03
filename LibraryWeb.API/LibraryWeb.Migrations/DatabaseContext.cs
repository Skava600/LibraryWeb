using LibraryWeb.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}