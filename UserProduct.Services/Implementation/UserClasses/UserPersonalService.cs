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
    public class UserPersonalService : IUserPersonalService
    {
        private readonly UserDbContext userDbContext;

        public UserPersonalService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<UserPersonal>> GetAllUserPersonals()
        {
            return await userDbContext.UserPersonals.Where(a => a.DeletedBy == null).ToListAsync();
        }


        public async Task<UserPersonal> GetUserPersonalsById(int id)
        {
            return await userDbContext.UserPersonals.Where(a =>a.UserPersonalId==id && a.DeletedBy == null).FirstOrDefaultAsync();
        }

        public async Task<UserPersonal> CreateUserPersonal(UserPersonal userPersonal)
        {
            await userDbContext.UserPersonals.AddAsync(userPersonal);
            await userDbContext.SaveChangesAsync();
            return userPersonal;
        }

        public async Task<UserPersonal> UpdateUserPersonal (UserPersonal userPersonal)
        {
            userDbContext.SaveChanges();
            return userPersonal;
        }

        public async Task<bool> DeleteUserPersonal(UserPersonal userPersonal)
        {
            userDbContext.UserPersonals.Remove(userPersonal);
            userDbContext.SaveChanges();
            return true;
        }

        public async  Task<UserPersonal> GetUserPersonalsByUserId(int id)
        {
            return await userDbContext.UserPersonals.FirstOrDefaultAsync(p => p.UserId == id && p.DeletedBy == null);
        }

        public async Task<UserPersonal> GetDeleteUserPersonalsByUserId(int id)
        {
            return await userDbContext.UserPersonals.FirstOrDefaultAsync(p => p.UserId == id && p.DeletedBy != null);
        }
    }
}
