using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.UserInterfaces;
using UserProduct.Services.Implementation.UserClasses;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Managers.Exceptions;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.Implmentattion.UserClasses
{
    public class UserAddressManager : IUserAddressManager
    {
        private readonly IUserAddressService userAddressService;
        private readonly IUserService userService;
        private readonly DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
       
        public UserAddressManager(IUserAddressService userAddressService, IUserService userService)
        {
            this.userAddressService = userAddressService;
            this.userService = userService;
        }

        public async Task<List<ResponseUserAddressDTO>> GetAllUsersAddress()
        {
            var res = await userAddressService.GetAllUsersAddress();
            return res.Select(x => ResponseUserAddressDTO.MapTouserAddressDTO(x)).ToList();
        }

        public async Task<ResponseUserAddressDTO> GetUserAddressById(int id)
        {
            List<string> exception = [];
            exception.Clear();
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await userAddressService.GetUserAddressById(id);
            if (res == null)
                exception.Add("User Address is Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return ResponseUserAddressDTO.MapTouserAddressDTO(res);
        }

        public async Task<ResponseUserAddressDTO> UpsertUserAddress(int? id, RequestUserAddressDTO userAddressDTO)
        {
            List<string> exception = [];
            if ( string.IsNullOrEmpty(userAddressDTO.City.Trim()) || string.IsNullOrEmpty(userAddressDTO.State.Trim()) || userAddressDTO.Zipcode.ToString().Trim().Length != 6 )
                exception.Add("Enter Valid Details");

            var user = await userService.GetUserById(userAddressDTO.UserId);
            var createdByUser = await userService.GetUserById(userAddressDTO.CreatedBy);
            if (user == null || createdByUser == null)
                exception.Add("User is Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            if (id == null)
            {
                var deletedUserAddress = await userAddressService.GetDeletedUserAddressByUserId(userAddressDTO.UserId);
                if (deletedUserAddress != null)
                {
                    deletedUserAddress.City = userAddressDTO.City;
                    deletedUserAddress.State = userAddressDTO.State;
                    deletedUserAddress.Zipcode = userAddressDTO.Zipcode;
                    deletedUserAddress.CreatedBy= userAddressDTO.CreatedBy;
                    deletedUserAddress.CreatedOn=currentDate;
                    deletedUserAddress.ChangedBy = null;
                    deletedUserAddress.ChangedOn = null;
                    deletedUserAddress.DeletedBy = null;
                    deletedUserAddress.DeletedOn = null;

                    var result = await userAddressService.UpdateUserAddress(deletedUserAddress);

                    return ResponseUserAddressDTO.MapTouserAddressDTO(result);
                }

                //var userAddressByUserId = await userAddressService.GetUserAddressByUserId(userAddressDTO.UserId);
                //if (userAddressByUserId!= null)
                //    exception.Add("User Address for user is Already exist");

                if (exception.Count != 0)
                    throw new ValidationException(String.Join(",\n", exception));

                var userAddress = RequestUserAddressDTO.MapTouserAddress(userAddressDTO);
                userAddress.CreatedOn = currentDate;

                var res = await userAddressService.CreateUserAddress(userAddress);
                return ResponseUserAddressDTO.MapTouserAddressDTO(res);
            }
            else
            {

                var dbUserAddress = await userAddressService.GetUserAddressById((int)id);
                if (dbUserAddress == null)
                    exception.Add("User Address is not present");

                if(userAddressDTO.ChangedBy != null)
                {
                    var changedByUser = await userService.GetUserById((int)userAddressDTO.ChangedBy);
                    if (changedByUser == null)
                        exception.Add("User who changes is not present");
                }
                else
                {
                    exception.Add("ChangedBy can't be Null");
                }

                if (dbUserAddress != null && dbUserAddress.UserId != userAddressDTO.UserId)
                    exception.Add("userid don't match for userid from database UserAddress");

                if (exception.Count != 0)
                    throw new ValidationException(String.Join(",\n", exception));

                dbUserAddress.City = userAddressDTO.City;
                dbUserAddress.State = userAddressDTO.State;
                dbUserAddress.Zipcode = userAddressDTO.Zipcode;
                dbUserAddress.ChangedBy = userAddressDTO.ChangedBy;
                dbUserAddress.ChangedOn = currentDate;

                var res = await userAddressService.UpdateUserAddress(dbUserAddress);

                return ResponseUserAddressDTO.MapTouserAddressDTO(res);
            }
            
        }

        public async Task<bool> DeleteUserAddress(int id, int deletedBy)
        {
            List<string> exception = [];
            if (id <= 0 || deletedBy <= 0) 
                exception.Add("Enter Valid Details");

            var userAddress = await userAddressService.GetUserAddressById(id);
            if (userAddress == null)
                exception.Add("User Address Does Not Exist");

            var deletedByUser = await userService.GetUserById(deletedBy);
            if (deletedByUser == null)
                exception.Add("User who deleting Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            userAddress.DeletedBy = deletedBy;
            userAddress.DeletedOn = currentDate;

            var res = await userAddressService.UpdateUserAddress(userAddress);
            if (res == null)
                return false;

            return true;
        }

        public async Task<bool> DeleteUserAddressByUserId(int id, int deletedBy)
        {
            UserAddress userAddresse = await userAddressService.GetUserAddressByUserId(id);
            if (userAddresse == null)
                return false;
            userAddresse.DeletedBy = deletedBy;
            userAddresse.DeletedOn = currentDate;

            UserAddress dbUserAddress = await userAddressService.UpdateUserAddress(userAddresse);
            return true;
        }

    }
}
