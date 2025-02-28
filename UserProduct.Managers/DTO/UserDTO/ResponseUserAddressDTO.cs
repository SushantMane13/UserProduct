
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class ResponseUserAddressDTO
    {
        public int UserAddressId { get; set; }
        public int UserId { get; set; }
        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public int Zipcode { get; set; }

        public int CreatedBy { get; set; }

        public int? ChangedBy { get; set; }

        public bool IsPrimary { get; set; }
        

        public static ResponseUserAddressDTO MapTouserAddressDTO(UserAddress userAddress)
        {
            return new ResponseUserAddressDTO
            {
                UserAddressId = userAddress.UserAddressId,
                UserId = userAddress.UserId,
                City = userAddress.City,
                State = userAddress.State,
                Zipcode = userAddress.Zipcode,
                CreatedBy = userAddress.CreatedBy,
                ChangedBy = userAddress.ChangedBy,
                IsPrimary = userAddress.IsPrimary,
            };
        }
    }
}
