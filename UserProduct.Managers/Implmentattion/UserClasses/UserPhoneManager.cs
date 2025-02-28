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
using UserProduct.Services.Implementation.UserClasses;

namespace UserProduct.Managers.Implmentattion.UserClasses
{
    public class UserPhoneManager : IUserPhoneManager
    {
        private readonly IUserPhoneService userPhoneService;
        private readonly IUserService userService;
        private readonly DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        public UserPhoneManager(IUserPhoneService userPhoneService,IUserService userService)
        {
            this.userPhoneService = userPhoneService;
            this.userService = userService;
        }

        public async Task<List<ResponseUserPhoneDTO>> GetAllUserPhones()
        {
            var res = await userPhoneService.GetAllUserPhones();

            return res.Select(x => ResponseUserPhoneDTO.MapToUserPhoneDTO(x)).ToList();
        }

        public async Task<ResponseUserPhoneDTO> GetUserPhoneById(int id)
        {
            List<string> exception = [];

            if (id <= 0)
                exception.Add("Enter Correct id");

            var res = await userPhoneService.GetUserPhoneById(id);
            if (res == null)
                exception.Add("User Phone is Not Found");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return ResponseUserPhoneDTO.MapToUserPhoneDTO(res);
        }

        public async Task<ResponseUserPhoneDTO> UpsertUserPhone(int? id, RequestUserPhoneDTO userPhoneDTO)
        {
            List<string> exception = [];

            if (!userPhoneDTO.Phone.All(char.IsDigit) || userPhoneDTO.Phone.Length != 10)
                exception.Add("Enter valid Details");

            var user = await userService.GetUserById(userPhoneDTO.UserId);
            var createdByUser = await userService.GetUserById(userPhoneDTO.CreatedBy);
            if (user == null || createdByUser == null)
                exception.Add("User is Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var primaryUserPhone= await userPhoneService.GetPrimaryUserPhoneByUserId(userPhoneDTO.UserId);

            var userPhone = await userPhoneService.GetUserPhoneByPhone(userPhoneDTO.Phone);
            if (id == null)
            {
                if (userPhone != null)
                    exception.Add("Phone is Already Exist");

                if (userPhoneDTO.IsPrimary == true && primaryUserPhone != null)
                    exception.Add("Primary Phone is already Exist");

                if (exception.Count != 0)
                    throw new ValidationException(String.Join(",\n", exception));

                userPhone = RequestUserPhoneDTO.MapToUserPhone(userPhoneDTO);
                userPhone.CreatedOn = currentDate;
                var res = await userPhoneService.CreateUserPhone(userPhone);
                return ResponseUserPhoneDTO.MapToUserPhoneDTO(res);
            }
            else
            {
                if (userPhone != null && userPhone.UserPhoneId != id)
                    exception.Add("Phone is Already Exist");

                if(userPhoneDTO.ChangedBy != null)
                {
                    var changedByUser = await userService.GetUserById((int)userPhoneDTO.ChangedBy);
                    if (changedByUser == null)
                        exception.Add("User who changes is not present");
                }
                else
                {
                    exception.Add("ChangedBy can't be Null");
                }

                if (userPhoneDTO.IsPrimary == true && primaryUserPhone != null && primaryUserPhone.UserPhoneId != id)
                    exception.Add("Primary Phone is already Exist");

                var dbUserPhone = await userPhoneService.GetUserPhoneById((int)id);
                if (dbUserPhone == null)
                    exception.Add("UserPhone is Not Exist");

                if (dbUserPhone != null && dbUserPhone.UserId != userPhoneDTO.UserId)
                    exception.Add("userid don't match for userid from database UserEmail");

                if (exception.Count != 0)
                    throw new ValidationException(String.Join(",\n", exception));

                dbUserPhone.Phone=userPhoneDTO.Phone;
                dbUserPhone.ChangedBy=userPhoneDTO.ChangedBy;
                dbUserPhone.IsPrimary = userPhoneDTO.IsPrimary;
                dbUserPhone.ChangedOn=currentDate;
                var res = await userPhoneService.UpdateUserPhone(dbUserPhone);
                return ResponseUserPhoneDTO.MapToUserPhoneDTO(res);
            }
            
            
        }

        

        public async Task<bool> DeleteUserPhone(int id, int deletedBy)
        {
            List<string> exception = [];

            if (id <= 0 || deletedBy <= 0)
                exception.Add("Enter Valid Details");

            var userPhone = await userPhoneService.GetUserPhoneById(id);
            if(userPhone == null)
                exception.Add("User Phone is not exists");

            var deletedByUser = await userService.GetUserById(deletedBy);
            if (deletedByUser == null)
                exception.Add("User who deleting Does not Exists");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            userPhone.DeletedBy = deletedBy;
            userPhone.DeletedOn = currentDate;

            var res = await userPhoneService.UpdateUserPhone(userPhone);
            if (res == null)
                return false;

            return true;
        }

        public async Task<bool> DeleteUserPhoneByUserId(int userId, int deletedBy)
        {
            List<UserPhone> userPhones=await userPhoneService.GetUserPhoneByUserId(userId);

            foreach (var item in userPhones)
            {
                item.DeletedBy = deletedBy;
                item.DeletedOn = currentDate;
                var res = await userPhoneService.UpdateUserPhone(item);
            }
            return true;
        }
    }
}
