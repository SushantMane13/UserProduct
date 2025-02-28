using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.UserDTO;

namespace UserProduct.Managers.Interface.UserInterfaces
{
    public interface IUserManager
    {
       Task<List<ResponseUserDTO>> GetAllUsers();

        Task<ResponseUserDTO> GetUserById(int id);

        Task<RequestUserDTO> CreateUser(RequestUserDTO userDTO);

        Task<RequestUserDTO> UpdateUser(int id, RequestUserDTO userDTO);

        Task<bool> DeleteUser(int id, string deletedBy, int deletedById);

        Task<object> Login(string username, string password);
    }
}
