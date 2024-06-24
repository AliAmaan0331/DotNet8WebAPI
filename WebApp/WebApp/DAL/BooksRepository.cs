using Microsoft.EntityFrameworkCore;
using WebApp.DAL.Interfaces;
using WebApp.Models;

namespace WebApp.DAL
{
    public class BooksRepository:IBooksRepository
    {
        private Result result = new Result();
        private readonly DataContext _context;

        public BooksRepository(DataContext context)
        {
            _context = context;
        }

        public Result GetBooks() 
        {
            result.Data = _context.Books.ToList();
            result.Success = true;
            result.Message = "Success";
            return result;
        }

        public Result AddBooks(Book book)
        {
            if (book.Id == 0)
            {
                result.Data = null;
                result.Success = false;
                result.Message = "Id can not be 0";
                return result;
            }
            _context.Books.Add(book);
            int changes = _context.SaveChanges();
            result.Data = null;
            result.Success = changes > 0 ? true : false;
            result.Message = changes > 0 ? "Success" : "Failed";
            return result;
        }

        public Result RemoveBooks(int id)
        {
            Book book = _context.Books.First(bk => bk.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                result.Success = true;
                result.Data = null;
                result.Message = "Success";
            }
            else
            {
                result.Data = null;
                result.Success = false;
                result.Message = "Record to be deleted does not exist";
            }
            return result;
        }

        public Result UpdateBooks(Book book)
        {
            var bookToUpdate = _context.Books.First(bk => bk.Id == book.Id);
            if (bookToUpdate == null)
            {
                result.Success = false;
                result.Data = null;
                result.Message = "Book to update does not exist";
                return result;
            }
            bookToUpdate.Name = book.Name;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Subject = book.Subject;
            bookToUpdate.CreatedBy = book.CreatedBy;
            bookToUpdate.CreatedOn = book.CreatedOn;
            bookToUpdate.LastModifiedBy = book.LastModifiedBy;
            bookToUpdate.LastModifiedOn = book.LastModifiedOn;
            _context.SaveChanges();
            result.Data = null;
            result.Success = true;
            result.Message = "Success";
            return result;
        }
    }
}