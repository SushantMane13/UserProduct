using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class RequestUserPersonalDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Education { get; set; } = null!;

        public int CreatedBy { get; set; }

        public int? ChangedBy { get; set; }

        public static UserPersonal MapToUserPersonal(RequestUserPersonalDTO userPersonalDTO)
        {
            return new UserPersonal
            {
                UserId = userPersonalDTO.UserId,
                FirstName = userPersonalDTO.FirstName,
                MiddleName = userPersonalDTO.MiddleName,
                LastName = userPersonalDTO.LastName,
                Dob = userPersonalDTO.Dob,
                Education = userPersonalDTO.Education,
                CreatedBy = userPersonalDTO.CreatedBy

            };
        }
    }
}
