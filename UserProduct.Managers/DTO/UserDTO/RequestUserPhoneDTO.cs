using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class RequestUserPhoneDTO
    {
        public int UserId { get; set; }
        public string Phone { get; set; } = null!;

        public int CreatedBy { get; set; }


        public int? ChangedBy { get; set; }


        public bool IsPrimary { get; set; }

        public static UserPhone MapToUserPhone(RequestUserPhoneDTO userPhoneDTO)
        {
            return new UserPhone
            {
                UserId = userPhoneDTO.UserId,
                Phone = userPhoneDTO.Phone,
                CreatedBy = userPhoneDTO.CreatedBy,
                ChangedBy = null,
                IsPrimary = userPhoneDTO.IsPrimary
            };
        }
    }
}
