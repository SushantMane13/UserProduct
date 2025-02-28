using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.UserInterfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(User user);

        Task<User> GetUserByUsername(string username);
    }
}
