using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class RequestUserAddressDTO
    {
        public int UserId { get; set; }
        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public int Zipcode { get; set; }

        public int CreatedBy { get; set; }

        public int? ChangedBy { get; set; }

        public bool IsPrimary { get; set; }

        public static UserAddress MapTouserAddress(RequestUserAddressDTO userAddressDTO)
        {
            return new UserAddress
            {
                UserId = userAddressDTO.UserId,
                City = userAddressDTO.City,
                State = userAddressDTO.State,
                Zipcode = userAddressDTO.Zipcode,
                CreatedBy = userAddressDTO.CreatedBy,
                ChangedBy = userAddressDTO.ChangedBy,
                IsPrimary = userAddressDTO.IsPrimary,
            };
        }
    }
}
