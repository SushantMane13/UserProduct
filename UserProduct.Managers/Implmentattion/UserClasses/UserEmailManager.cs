using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.UserInterfaces;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Services.Models.Entity;
using UserProduct.Managers.Exceptions;
using System.Text.RegularExpressions;

namespace UserProduct.Managers.Implmentattion.UserClasses
{
    public class UserEmailManager : IUserEmailManager
    {
        private readonly IUserEmailService userEmailService;
        private readonly IUserService userService;
        private readonly DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        public UserEmailManager(IUserEmailService userEmailService, IUserService userService)
        {
            this.userEmailService = userEmailService;
            this.userService = userService;
        }

        public async Task<List<ResponseUserEmailDTO>> GetAllUserEmail()
        {
            var res = await userEmailService.GetAllUserEmails();
            return res.Select(x => ResponseUserEmailDTO.MapToUserEmailDTO(x)).ToList();
        }

        public async Task<ResponseUserEmailDTO> GetUserEmailById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter valid id");

            var res = await userEmailService.GetUserEmailById(id);
            if (res == null)
                exception.Add("User Email Not Found");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return ResponseUserEmailDTO.MapToUserEmailDTO(res);
        }

        public async Task<ResponseUserEmailDTO> UpsertUserEmail(int? id, RequestUserEmailDTO userEmailDTO)
        {
            List<string> exception = [];

            bool isEmail = Regex.IsMatch(userEmailDTO.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if ( !isEmail || userEmailDTO.CreatedBy<=0 || userEmailDTO.UserId<=0)
                exception.Add("Enter valid Details");

            var user = await userService.GetUserById(userEmailDTO.UserId);
            var createdByUser = await userService.GetUserById(userEmailDTO.CreatedBy);
            if (user == null || createdByUser == null)
                exception.Add("User is not present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var primaryEmail = await userEmailService.GetPrimaryUserEmailByUserId(userEmailDTO.UserId);
            var userEmail = await userEmailService.GetUserEmailByEmail(userEmailDTO.Email);
            if (id == null)
            {
                
                if (userEmail != null)
                    exception.Add("Email is Already Exist");
                

                if (userEmailDTO.IsPrimary == true && primaryEmail != null)
                    exception.Add("Primary Email Already Exist");

                if (exception.Count != 0)
                    throw new ValidationException(String.Join(",\n", exception));


                userEmail = RequestUserEmailDTO.MapToUserEmail(userEmailDTO);
                userEmail.CreatedOn = currentDate;

                var res = await userEmailService.CreateUserEmail(userEmail);
                return ResponseUserEmailDTO.MapToUserEmailDTO(res);
            }
            else
            {
                if (userEmail != null && userEmail.UserEmailId!=id)
                    exception.Add("Email is Already Exist");

                if(userEmailDTO.ChangedBy != null)
                {
                    var changedByUser = await userService.GetUserById((int)userEmailDTO.ChangedBy);
                    if ( changedByUser== null)
                        exception.Add("User who changes is not present");
                }
                else
                {
                    exception.Add("ChangedBy can't be Null");
                }

                if (userEmailDTO.IsPrimary == true && primaryEmail != null && primaryEmail.UserEmailId!=id)
                    exception.Add("Primary Email Already Exist");

                var dbUserEmail = await userEmailService.GetUserEmailById((int)id);
                if (dbUserEmail == null)
                    exception.Add("UserEmail is not exist");

                if (dbUserEmail != null && dbUserEmail.UserId != userEmailDTO.UserId)
                    exception.Add("userid don't match for userid from database UserEmail");

                if (exception.Count != 0)
                    throw new ValidationException(String.Join(",\n", exception));

                dbUserEmail.Email = userEmailDTO.Email;
                dbUserEmail.IsPrimary = userEmailDTO.IsPrimary;
                dbUserEmail.ChangedBy = userEmailDTO.ChangedBy;
                dbUserEmail.ChangedOn = currentDate;

                var res = await userEmailService.UpdateUserEmail(dbUserEmail);

                return ResponseUserEmailDTO.MapToUserEmailDTO(res);
            }
            
        }



        

        public async Task<bool> DeleteUserEmail(int id, int deletedBy)
        {
            List<string> exception = [];

            if (id <= 0 || deletedBy <= 0)
                exception.Add("Enter Valid Details");

            var userEmail = await userEmailService.GetUserEmailById(id);
            
            if(userEmail==null)
                exception.Add("User Email Not Found");

            var deletedByUser = await userService.GetUserById(deletedBy);
            if (deletedByUser == null)
                exception.Add("User who deleting is not present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            userEmail.DeletedBy = deletedBy;
            userEmail.DeletedOn = currentDate;

            var res = await userEmailService.UpdateUserEmail(userEmail);
            if (res == null)
                return false;

            return true;
        }

        public async Task<bool> DeleteUserEmailByUserId(int userId, int deletedBy)
        {
            List<UserEmail> userEmails = await userEmailService.GetUserEmailsByUserId(userId);

            foreach (var item in userEmails)
            {
                item.DeletedBy = deletedBy;
                item.DeletedOn = currentDate;
                var res= await userEmailService.UpdateUserEmail(item);
            }
            return true;
        }
    }
}
