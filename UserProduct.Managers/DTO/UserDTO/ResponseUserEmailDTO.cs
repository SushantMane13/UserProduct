
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class ResponseUserEmailDTO
    {
        public int UserEmailId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } = null!;

        public int CreatedBy { get; set; }

        public bool IsPrimary { get; set; }

        public int? ChangedBy { get; set; }


        public static ResponseUserEmailDTO MapToUserEmailDTO(UserEmail userEmail)
        {
            return new ResponseUserEmailDTO
            {
                UserEmailId = userEmail.UserEmailId,
                UserId = userEmail.UserId,
                Email = userEmail.Email,
                CreatedBy = userEmail.CreatedBy,
                IsPrimary = userEmail.IsPrimary,
                ChangedBy = userEmail.ChangedBy
            };
        }

        
    }
}
