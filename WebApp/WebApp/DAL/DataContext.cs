using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.DAL
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
