using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DAL.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _usersRepository;
        private Result result = new Result();
        public UsersController(IUserRepository usersRepository) 
        {
            _usersRepository = usersRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<Result> Add([FromBody] User user)
        {
            try
            {
                result = _usersRepository.AddUser(user);
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
