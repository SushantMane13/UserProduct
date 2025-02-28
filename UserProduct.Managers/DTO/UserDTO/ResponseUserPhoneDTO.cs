
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class ResponseUserPhoneDTO
    {
        public int UserPhoneId { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; } = null!;

        public int CreatedBy { get; set; }


        public int? ChangedBy { get; set; }


        public bool IsPrimary { get; set; }

        public static ResponseUserPhoneDTO MapToUserPhoneDTO(UserPhone userPhone)
        {
            return new ResponseUserPhoneDTO
            {
                UserPhoneId = userPhone.UserPhoneId,
                UserId = userPhone.UserId,
                Phone = userPhone.Phone,
                CreatedBy = userPhone.CreatedBy,
                ChangedBy = userPhone.ChangedBy,
                IsPrimary = userPhone.IsPrimary
            };
        }

       
    }
}
