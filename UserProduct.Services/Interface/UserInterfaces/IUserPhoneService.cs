using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.UserInterfaces
{
    public interface IUserPhoneService
    {
        Task<List<UserPhone>> GetAllUserPhones();

        Task<UserPhone> GetUserPhoneById(int id);

        Task<UserPhone> CreateUserPhone(UserPhone userPhone);

        Task<UserPhone> UpdateUserPhone(UserPhone userPhone);

        Task<bool> DeleteUserPhone(UserPhone userPhone);

        Task<UserPhone> GetUserPhoneByPhone(string phone);

        Task<List<UserPhone>> GetUserPhoneByUserId(int userId);

        Task<UserPhone> GetPrimaryUserPhoneByUserId(int userId);

    }
}
