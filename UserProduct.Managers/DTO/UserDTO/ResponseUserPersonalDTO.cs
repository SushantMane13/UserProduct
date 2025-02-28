using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.UserDTO
{
    public class ResponseUserPersonalDTO
    {
        public int UserPersonalId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Education { get; set; } = null!;

        public int CreatedBy { get; set; }

        public int? ChangedBy { get; set; }


        public static ResponseUserPersonalDTO MapToUserPersonalDTO(UserPersonal userPersonal)
        {
            return new ResponseUserPersonalDTO
            {
                UserPersonalId = userPersonal.UserPersonalId,
                UserId = userPersonal.UserId,
                FirstName = userPersonal.FirstName,
                MiddleName = userPersonal.MiddleName,
                LastName = userPersonal.LastName,
                Dob = userPersonal.Dob,
                Education = userPersonal.Education,
                CreatedBy = userPersonal.CreatedBy,
                ChangedBy = userPersonal.ChangedBy,

            };
        }

        
    }
}
