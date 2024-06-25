using Microsoft.EntityFrameworkCore;
using WebApp.DAL.Interfaces;
using WebApp.Models;

namespace WebApp.DAL
{
    public class UserRepository:IUserRepository
    {
        private Result result = new Result();
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        {
            _context = context;
        }
        public Result AddUser(User user)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null || user.Id == 0)
                {
                    result.Data = null;
                    result.Success = false;
                    result.Message = user.Id == 0 ? "Unable to add user" : "User already exists";
                    return result;
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                result.Data = null;
                result.Success = true;
                result.Message = "User added successfully";
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
