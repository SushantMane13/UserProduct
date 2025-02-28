using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Services.Models;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Implementation.UserClasses
{
    public class UserService : IUserService
    {
        private readonly UserDbContext userDbContext;

        public UserService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var res= await userDbContext.Users.Where(a => a.DeletedBy == null).Include(a => a.UserPersonalUser).Where(p => p.UserPersonalUser.DeletedBy == null)
                .Include(a => a.UserPhoneUsers).Include(a=>a.UserAddressUser)
                .Include(a=>a.UserEmailUsers).ToListAsync();


             res.ForEach(a => a.UserAddressUser = a.UserAddressUser.Where(x => x.DeletedBy == null).ToList());
             res.ForEach(x => x.UserEmailUsers=x.UserEmailUsers.Where(a => a.DeletedBy == null).ToList());
            res.ForEach(x => x.UserPhoneUsers=x.UserPhoneUsers.Where(p => p.DeletedBy == null).ToList());
            return res;
        }


        public async Task<User> GetUserById(int id)
        {
            var res= await userDbContext.Users.Where(a => a.UserId == id && a.DeletedBy == null).Include(a => a.UserPersonalUser).Where(p => p.UserPersonalUser.DeletedBy == null)
                .Include(a => a.UserPhoneUsers).Include(a => a.UserAddressUser)
                .Include(a => a.UserEmailUsers).ToListAsync();


            res.ForEach(a => a.UserAddressUser = a.UserAddressUser.Where(x => x.DeletedBy == null).ToList());
            res.ForEach(x => x.UserEmailUsers = x.UserEmailUsers.Where(a => a.DeletedBy == null).ToList());
            res.ForEach(x => x.UserPhoneUsers = x.UserPhoneUsers.Where(p => p.DeletedBy == null).ToList());

            return res.FirstOrDefault(); 
        }
        
        public async Task<User> CreateUser(User user)
        {
            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            userDbContext.SaveChanges();
            return user;
        }

        public async Task<bool> DeleteUser(User user)
        {
            userDbContext.Users.Remove(user);
            userDbContext.SaveChanges();
            return true;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var res= await userDbContext.Users.Where(u => u.Username == username && u.DeletedBy==null).Include(a => a.UserPersonalUser).Where(p => p.UserPersonalUser.DeletedBy == null)
                .Include(a => a.UserPhoneUsers).Include(a => a.UserAddressUser)
                .Include(a => a.UserEmailUsers).FirstOrDefaultAsync();

            res.UserAddressUser = res.UserAddressUser.Where(x => x.DeletedBy == null).ToList();
            res.UserEmailUsers = res.UserEmailUsers.Where(a => a.DeletedBy == null).ToList();
            res.UserPhoneUsers = res.UserPhoneUsers.Where(p => p.DeletedBy == null).ToList();
            return res;

        }
    }
}
