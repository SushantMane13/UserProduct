using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;

namespace UserProduct.Managers.Interface.UserInterfaces
{
    public interface IUserPersonalManager
    {
        Task<List<ResponseUserPersonalDTO>> GetAllUserPersonals();

        Task<ResponseUserPersonalDTO> GetUserPersonalById(int id);

        Task<ResponseUserPersonalDTO> UpsertUserPersonal(int? id,RequestUserPersonalDTO userPersonalDTO);


        Task<bool> DeleteUserPersonal(int id,int deletedBy);

        Task<bool> DeleteUserPersonalByUserId(int userId, int deletedBy);
    }
}
