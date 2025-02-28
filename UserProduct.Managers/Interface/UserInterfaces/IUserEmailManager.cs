using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;

namespace UserProduct.Managers.Interface.UserInterfaces
{
    public interface IUserEmailManager
    {
        Task<List<ResponseUserEmailDTO>> GetAllUserEmail();

        Task<ResponseUserEmailDTO> GetUserEmailById(int id);

        Task<ResponseUserEmailDTO> UpsertUserEmail(int? id,RequestUserEmailDTO userEmailDTO);


        Task<bool> DeleteUserEmail(int id,int deletedBy);

        Task<bool> DeleteUserEmailByUserId(int userId, int deletedBy);
    }
}
