using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class CitizenSignUpOutputDTO
    {
        public CitizenSignUpOutputDTO()
        {
            
        }
        public CitizenSignUpOutputDTO(Citizen citizen)
        {
            Id = citizen.Id;
        }

        public int Id { get; set; }
    }
}
