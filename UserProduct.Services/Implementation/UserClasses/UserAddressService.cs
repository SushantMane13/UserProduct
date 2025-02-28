using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Services.Models;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Implementation.UserClasses
{
    public class UserAddressService : IUserAddressService
    {
        private readonly UserDbContext userDbContext;

        public UserAddressService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<UserAddress>> GetAllUsersAddress()
        {
            return await userDbContext.UserAddresses.Where(a => a.DeletedBy == null).ToListAsync();
        }


        public async Task<UserAddress> GetUserAddressById(int id)
        {
            return await userDbContext.UserAddresses.Where(a =>a.UserAddressId==id && a.DeletedBy == null).FirstOrDefaultAsync();
        }

        public async Task<UserAddress> CreateUserAddress(UserAddress userAddress)
        {
            await userDbContext.UserAddresses.AddAsync(userAddress);
            await userDbContext.SaveChangesAsync();
            return userAddress;
        }

        public async Task<UserAddress> UpdateUserAddress(UserAddress userAddress)
        {
            userDbContext.SaveChanges();
            return userAddress;
        }

        public async Task<bool> DeleteUserAddress(UserAddress userAddress)
        {
            userDbContext.UserAddresses.Remove(userAddress);
            userDbContext.SaveChanges();
            return true;
        }

        public async Task<UserAddress> GetUserAddressByUserId(int id)
        {
            return await userDbContext.UserAddresses.Where(a => a.UserId == id && a.DeletedBy==null).FirstOrDefaultAsync();
            
        }

        public async Task<UserAddress> GetDeletedUserAddressByUserId(int id)
        {
            return await userDbContext.UserAddresses.FirstOrDefaultAsync(a => a.UserId == id && a.DeletedBy != null);

        }
    }
}
