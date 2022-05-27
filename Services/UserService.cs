using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPDP_Stefan.Services
{
    public class UserService : IUserService
    {
        private Context.ContextDb _context;

        public UserService(ContextDb context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public async Task DeleteUser(User user)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }
        public async Task<User> EditUser(User user)
        {
            var existingUser = _context.User.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.RealName = user.RealName;
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.CreationDate = user.CreationDate;

                _context.User.Update(existingUser);
                _context.SaveChanges();
            }
            return user;
        }
        public async Task<User> GetUserByUserName(string name)
        {
            return _context.User.SingleOrDefault(x => x.UserName == name);
        }
        public async Task<User> GetUserById(int id)
        {
            return _context.User.SingleOrDefault(x => x.Id == id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return _context.User.ToList();
        }
    }
}
