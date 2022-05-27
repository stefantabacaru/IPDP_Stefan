using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPDP_Stefan.Interfaces
{
    public interface IUserService 
    {
        public Task<User> AddUser(User user);
        public Task DeleteUser(User user);
        public Task<User> EditUser(User user);
        public Task<User> GetUserByUserName(string name);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
    }
}
