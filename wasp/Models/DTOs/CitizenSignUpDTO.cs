using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class CitizenSignUpDTO
    {
        public CitizenSignUpDTO()
        {
            
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
    }
}
