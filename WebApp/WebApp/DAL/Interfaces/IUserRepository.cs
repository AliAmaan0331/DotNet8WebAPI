using WebApp.Models;

namespace WebApp.DAL.Interfaces
{
    public interface IUserRepository
    {
        public Result AddUser(User user); 
    }
}
