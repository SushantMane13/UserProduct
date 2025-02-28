using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;

namespace UserProduct.Managers.Interface.UserInterfaces
{
    public interface IUserAddressManager
    {
        Task<List<ResponseUserAddressDTO>> GetAllUsersAddress();

        Task<ResponseUserAddressDTO> GetUserAddressById(int id);

        Task<ResponseUserAddressDTO> UpsertUserAddress(int? id,RequestUserAddressDTO userAddressDTO);


        Task<bool> DeleteUserAddress(int id,int deletedBy);

        Task<bool> DeleteUserAddressByUserId(int id, int deletedBy);
    }
}
