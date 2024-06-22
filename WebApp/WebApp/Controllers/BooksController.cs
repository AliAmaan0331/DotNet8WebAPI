using Microsoft.AspNetCore.Mvc;
using WebApp.DAL.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private Result result = new Result();

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<Result> Get()
        {
            try
            {
                result = _booksRepository.GetBooks();
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        [HttpPost(Name = "CreateBook")]
        public async Task<Result> Post(Book book)
        {
            try
            {
                result = _booksRepository.AddBooks(book);
                return result;
            }
            catch(Exception ex)
            {
                result.Data = null;
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        [HttpDelete(Name = "DeleteBook")]
        public async Task<Result> Delete(int id)
        {
            try
            {
                result = _booksRepository.RemoveBooks(id);
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        [HttpPut(Name = "UpdateBook")]
        public async Task<Result> Put(Book book)
        {
            try
            {
                result = _booksRepository.UpdateBooks(book);
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
