using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.UserInterfaces
{
    public interface IUserEmailService
    {
        Task<List<UserEmail>> GetAllUserEmails();

        Task<UserEmail> GetUserEmailById(int id);

        Task<UserEmail> CreateUserEmail(UserEmail userEmail);

        Task<UserEmail> UpdateUserEmail(UserEmail userEmail);

        Task<bool> DeleteUserEmail(UserEmail userEmail);

        Task<UserEmail> GetUserEmailByEmail(string email);

        Task<List<UserEmail>> GetUserEmailsByUserId(int userId);

        Task<UserEmail> GetPrimaryUserEmailByUserId(int userId);
    }
}
