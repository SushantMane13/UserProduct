using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class ResponseUserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public string? ChangedBy { get; set; } = null;

        public List<ResponseUserAddressDTO>? Address { get; set; }

        public List<ResponseUserEmailDTO>? Emails { get; set; }

        public ResponseUserPersonalDTO? Personal { get; set; }

        public List<ResponseUserPhoneDTO>? Phones { get; set; }

        public static ResponseUserDTO MapToUsersDTO(User user)
        {
            return new ResponseUserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                CreatedBy = user.CreatedBy,
                ChangedBy = user.ChangedBy,
                Address = user.UserAddressUser?.Select(x=>ResponseUserAddressDTO.MapTouserAddressDTO(x)).ToList(),
                Personal= user.UserPersonalUser is null ? null : ResponseUserPersonalDTO.MapToUserPersonalDTO(user.UserPersonalUser),
                Emails=user.UserEmailUsers?.Select(x=>ResponseUserEmailDTO.MapToUserEmailDTO(x)).ToList(),
                Phones=user.UserPhoneUsers?.Select(x=>ResponseUserPhoneDTO.MapToUserPhoneDTO(x)).ToList()
            };
        }

    }
}
