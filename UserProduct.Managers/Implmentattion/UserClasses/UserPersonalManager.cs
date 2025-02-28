using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.UserInterfaces;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Managers.Exceptions;
using UserProduct.Services.Models.Entity;


namespace UserProduct.Managers.Implmentattion.UserClasses
{
    public class UserPersonalManager : IUserPersonalManager
    {
        private readonly IUserPersonalService userPersonalService;
        private readonly IUserService userService;
        private readonly DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        public UserPersonalManager(IUserPersonalService userPersonalService,IUserService userService)
        {
            this.userPersonalService = userPersonalService;
            this.userService = userService;
        }

        public async Task<List<ResponseUserPersonalDTO>> GetAllUserPersonals()
        {
            var res = await userPersonalService.GetAllUserPersonals();
            return res.Select(x => ResponseUserPersonalDTO.MapToUserPersonalDTO(x)).ToList();
        }

        public async Task<ResponseUserPersonalDTO> GetUserPersonalById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Correct id");

            var res = await userPersonalService.GetUserPersonalsById(id);
            if (res == null)
                exception.Add("User Personal Is Not Present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));


            return ResponseUserPersonalDTO.MapToUserPersonalDTO(res);
        }

        public async Task<ResponseUserPersonalDTO> UpsertUserPersonal(int? id, RequestUserPersonalDTO userPersonalDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(userPersonalDTO.FirstName.Trim()) || string.IsNullOrEmpty(userPersonalDTO.LastName.Trim()) || string.IsNullOrEmpty(userPersonalDTO.MiddleName.Trim()))
                exception.Add("Enter valid Details");

            var user = await userService.GetUserById(userPersonalDTO.UserId);
            var createdByUser = await userService.GetUserById(userPersonalDTO.CreatedBy);
            if (user == null || createdByUser == null)
                exception.Add("User is not present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));


                if (id == null)
                {
                    var deletedUserPersonal = await userPersonalService.GetDeleteUserPersonalsByUserId(userPersonalDTO.UserId);
                    if(deletedUserPersonal != null)
                    {
                        deletedUserPersonal.FirstName = userPersonalDTO.FirstName;
                        deletedUserPersonal.MiddleName = userPersonalDTO.MiddleName;
                        deletedUserPersonal.LastName = userPersonalDTO.LastName;
                        deletedUserPersonal.Dob = userPersonalDTO.Dob;
                        deletedUserPersonal.Education = userPersonalDTO.Education;
                        deletedUserPersonal.CreatedBy = userPersonalDTO.CreatedBy;
                        deletedUserPersonal.CreatedOn = currentDate;
                        deletedUserPersonal.ChangedBy = null;
                        deletedUserPersonal.ChangedOn = null;
                        deletedUserPersonal.DeletedBy = null;
                        deletedUserPersonal.DeletedOn = null;
                        var result = await userPersonalService.UpdateUserPersonal(deletedUserPersonal);
                        return ResponseUserPersonalDTO.MapToUserPersonalDTO(result);
                    }
                var userPersonalByUserId = await userPersonalService.GetUserPersonalsByUserId(userPersonalDTO.UserId);
                    if (userPersonalByUserId != null)
                    exception.Add("User Address for user is Already exist");

                    if (exception.Count != 0)
                        throw new ValidationException(String.Join(",\n", exception));

                    var userPersonal = RequestUserPersonalDTO.MapToUserPersonal(userPersonalDTO);
                    userPersonal.CreatedOn = currentDate;
                    var res = await userPersonalService.CreateUserPersonal(userPersonal);
                    return ResponseUserPersonalDTO.MapToUserPersonalDTO(res);
                }
                else
                {
                    if(userPersonalDTO.ChangedBy != null)
                    {
                    var changedByUser = await userService.GetUserById((int)userPersonalDTO.ChangedBy);
                        if (changedByUser == null)
                            exception.Add("User who changes is not present");
                    }
                    else
                    {
                        exception.Add("ChangedBy can't be Null");
                    }

                    var dbUserPersonal = await userPersonalService.GetUserPersonalsById((int)id);
                    if (dbUserPersonal == null)
                        exception.Add("User Personal is not present");

                    if (dbUserPersonal != null && dbUserPersonal.UserId != userPersonalDTO.UserId)
                        exception.Add("userid don't match for userid from database UserPersonal");

                    if (exception.Count != 0)
                        throw new ValidationException(String.Join(",\n", exception));

                    dbUserPersonal.FirstName = userPersonalDTO.FirstName;
                    dbUserPersonal.MiddleName = userPersonalDTO.MiddleName;
                    dbUserPersonal.LastName = userPersonalDTO.LastName;
                    dbUserPersonal.Dob = userPersonalDTO.Dob;
                    dbUserPersonal.Education = userPersonalDTO.Education;
                    dbUserPersonal.ChangedBy = userPersonalDTO.ChangedBy;
                    dbUserPersonal.ChangedOn = currentDate;
                    var res = await userPersonalService.UpdateUserPersonal(dbUserPersonal);

                    return ResponseUserPersonalDTO.MapToUserPersonalDTO(res);
                }
            
            
        }

        

        public async Task<bool> DeleteUserPersonal(int id, int deletedBy)
        {
            List<string> exception = [];

            if (id <= 0 || deletedBy <= 0)
                exception.Add("Enter Valid Details");

            var userPersonal = await userPersonalService.GetUserPersonalsById(id);
            if(userPersonal==null)
                exception.Add("User Personal Not Found");

            var deletedByUser = await userService.GetUserById(deletedBy);
            if (deletedByUser == null)
                exception.Add("User who deleting is Not Present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            userPersonal.DeletedBy = deletedBy;
            userPersonal.DeletedOn = currentDate;

            var res = await userPersonalService.UpdateUserPersonal(userPersonal);
            if (res == null)
                return false;

            return true;
        }

        public async Task<bool> DeleteUserPersonalByUserId(int userId, int deletedBy)
        {
            UserPersonal userPersonal = await userPersonalService.GetUserPersonalsByUserId(userId);

            if (userPersonal == null)
                return false;

            userPersonal.DeletedBy=deletedBy;
            userPersonal.DeletedOn = currentDate;

            var res = await userPersonalService.UpdateUserPersonal(userPersonal);

            return true;
        }
    }
}
