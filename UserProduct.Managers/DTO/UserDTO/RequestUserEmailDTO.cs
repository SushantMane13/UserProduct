using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class RequestUserEmailDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;

        public int CreatedBy { get; set; }

        public bool IsPrimary { get; set; }

        public int? ChangedBy { get; set; }

        public static UserEmail MapToUserEmail(RequestUserEmailDTO userEmailDTO)
        {
            return new UserEmail
            {
                UserId = userEmailDTO.UserId,
                Email = userEmailDTO.Email,
                CreatedBy = userEmailDTO.CreatedBy,
                IsPrimary = userEmailDTO.IsPrimary,
                ChangedBy = null
            };
        }
    }
}
