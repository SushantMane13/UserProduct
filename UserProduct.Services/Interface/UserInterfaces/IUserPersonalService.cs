using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.UserInterfaces
{
    public interface IUserPersonalService
    {
        Task<List<UserPersonal>> GetAllUserPersonals();

        Task<UserPersonal> GetUserPersonalsById(int id);

        Task<UserPersonal> CreateUserPersonal(UserPersonal userPersonal);

        Task<UserPersonal> UpdateUserPersonal(UserPersonal userPersonal);

        Task<bool> DeleteUserPersonal(UserPersonal userPersonal);

        Task<UserPersonal> GetUserPersonalsByUserId(int id);

        Task<UserPersonal> GetDeleteUserPersonalsByUserId(int id);

    }
}
