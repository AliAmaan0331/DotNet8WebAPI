using WebApp.Models;

namespace WebApp.DAL.Interfaces
{
    public interface IBooksRepository
    {
        public Result GetBooks();
        public Result AddBooks(Book book);
        public Result RemoveBooks(int id);
        public Result UpdateBooks(Book book);
    }
}
