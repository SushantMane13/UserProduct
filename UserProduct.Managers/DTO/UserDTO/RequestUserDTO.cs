using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class RequestUserDTO
    {
        public int UserId {  get; set; }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public string? ChangedBy { get; set; }

        public static User MapToUser(RequestUserDTO usersDTO)
        {
            return new User
            {
                Username = usersDTO.Username,
                Password = usersDTO.Password,
                CreatedBy = usersDTO.CreatedBy,
                ChangedBy = null
            };
        }

        public static RequestUserDTO MapToUsersDTO(User user)
        {
            return new RequestUserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                CreatedBy = user.CreatedBy,
                ChangedBy = user.ChangedBy
            };
        }

        
    }
}
