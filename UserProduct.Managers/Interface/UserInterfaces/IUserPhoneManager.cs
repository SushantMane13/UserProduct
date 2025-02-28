using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;

namespace UserProduct.Managers.Interface.UserInterfaces
{
    public interface IUserPhoneManager
    {
        Task<List<ResponseUserPhoneDTO>> GetAllUserPhones();

        Task<ResponseUserPhoneDTO> GetUserPhoneById(int id);

        Task<ResponseUserPhoneDTO> UpsertUserPhone(int? id,RequestUserPhoneDTO userPhoneDTO);


        Task<bool> DeleteUserPhone(int id, int deletedBy);

        Task<bool> DeleteUserPhoneByUserId(int userId, int deletedBy);
    }
}
