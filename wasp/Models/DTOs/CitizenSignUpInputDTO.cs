using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class CitizenSignUpInputDTO
    {
        public CitizenSignUpInputDTO()
        {
            
        }

        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
    }
}
