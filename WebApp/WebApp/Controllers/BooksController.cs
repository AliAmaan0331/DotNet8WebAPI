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

        [HttpGet]
        [Route("Get")]
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

        [HttpPost]
        [Route("Add")]
        public async Task<Result> Post([FromBody] Book book)
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

        [HttpDelete]
        [Route("Delete")]
        public async Task<Result> Delete([FromBody] int id)
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

        [HttpPut]
        [Route("Update")]
        public async Task<Result> Put([FromBody] Book book)
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

        [HttpGet]
        [Route("GetByUserId")]
        public async Task<Result> GetBooksByUserId(int userId)
        {
            try
            {
                result = _booksRepository.GetBooksByUserId(userId);
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
