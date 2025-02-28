using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Models.Entity;
using UserProduct.Services.Models;
using UserProduct.Services.Interface.UserInterfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace UserProduct.Services.Implementation.UserClasses
{
    public class UserEmailService : IUserEmailService
    {
        private readonly UserDbContext userDbContext;

        public UserEmailService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<UserEmail>> GetAllUserEmails()
        {
            return await userDbContext.UserEmails.Where(a => a.DeletedBy == null).ToListAsync();
        }


        public async Task<UserEmail> GetUserEmailById(int id)
        {
            return await userDbContext.UserEmails.Where(a =>a.UserEmailId==id && a.DeletedBy == null).FirstOrDefaultAsync();
        }

        public async Task<UserEmail> CreateUserEmail(UserEmail userEmail)
        {
            await userDbContext.UserEmails.AddAsync(userEmail);
            await userDbContext.SaveChangesAsync();
            return userEmail;
        }

        public async Task<UserEmail> UpdateUserEmail(UserEmail userEmail)
        {
            await userDbContext.SaveChangesAsync();
            return userEmail;
        }

        public async Task<bool> DeleteUserEmail(UserEmail userEmail)
        {
            userDbContext.UserEmails.Remove(userEmail);
            userDbContext.SaveChanges();
            return true;
        }

        public async Task<UserEmail> GetUserEmailByEmail(string email)
        {
            return await userDbContext.UserEmails.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<List<UserEmail>> GetUserEmailsByUserId(int userId)
        {
            return await userDbContext.UserEmails.Where(e=>e.UserId == userId && e.DeletedBy == null).ToListAsync();
        }

        public async Task<UserEmail> GetPrimaryUserEmailByUserId(int userId)
        {
            return await userDbContext.UserEmails.FirstOrDefaultAsync(e => e.UserId == userId && e.DeletedBy == null && e.IsPrimary==true);
        }
    }
}
