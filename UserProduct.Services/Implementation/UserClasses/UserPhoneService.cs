using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Services.Models.Entity;
using UserProduct.Services.Models;

namespace UserProduct.Services.Implementation.UserClasses
{
    public class UserPhoneService : IUserPhoneService
    {
        private readonly UserDbContext userDbContext;

        public UserPhoneService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<UserPhone>> GetAllUserPhones()
        {
            return await userDbContext.UserPhones.Where(a => a.DeletedBy == null).ToListAsync();
        }


        public async Task<UserPhone> GetUserPhoneById(int id)
        {
            return await userDbContext.UserPhones.Where(a =>a.UserPhoneId==id && a.DeletedBy == null).FirstOrDefaultAsync();
        }

        public async Task<UserPhone> CreateUserPhone(UserPhone userPhone)
        {
            await userDbContext.UserPhones.AddAsync(userPhone);
            await userDbContext.SaveChangesAsync();
            return userPhone;
        }

        public async Task<UserPhone> UpdateUserPhone(UserPhone userPhone)
        {
            userDbContext.SaveChanges();
            return userPhone;
        }

        public async Task<bool> DeleteUserPhone(UserPhone userPhone)
        {
            userDbContext.UserPhones.Remove(userPhone);
            userDbContext.SaveChanges();
            return true;
        }

        public async Task<UserPhone> GetUserPhoneByPhone(string phone)
        {
            return await userDbContext.UserPhones.Where(p=>p.Phone==phone ).FirstOrDefaultAsync();
        }

        public async Task<List<UserPhone>> GetUserPhoneByUserId(int userId)
        {
            return await userDbContext.UserPhones.Where(p => p.UserId == userId && p.DeletedBy == null).ToListAsync();
        }

        public async Task<UserPhone> GetPrimaryUserPhoneByUserId(int userId)
        {
            return await userDbContext.UserPhones.FirstOrDefaultAsync(p => p.UserId == userId && p.DeletedBy == null && p.IsPrimary==true);
        }
    }
}
