using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.UserInterfaces
{
    public interface IUserAddressService
    {
        Task<List<UserAddress>> GetAllUsersAddress();

        Task<UserAddress> GetUserAddressById(int id);

        Task<UserAddress> CreateUserAddress(UserAddress userAddress);

        Task<UserAddress> UpdateUserAddress(UserAddress userAddress);

        Task<bool> DeleteUserAddress(UserAddress userAddress);

        Task<UserAddress> GetUserAddressByUserId(int id);

        Task<UserAddress> GetDeletedUserAddressByUserId(int id);
    }
}
