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
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Book)
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            modelBuilder.Entity<User>().HasData(
                new User() { Id=1, Name="Test", CNIC="123456", CreatedBy="Admin", CreatedOn=DateTime.Now, LastModifiedBy="", LastModifiedOn=null}
                );
        }
    }
}
