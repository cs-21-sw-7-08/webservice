using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class CitizenLoginDTO
    {
        public CitizenLoginDTO()
        {

        }
        public CitizenLoginDTO(Citizen citizen)
        {
            Email = citizen.Email;
            PhoneNo = citizen.PhoneNo;
        }
            public string Email { get; set; }
            public string PhoneNo { get; set; }
    }
}
