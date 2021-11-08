using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class MunicipalityUserSignUpInputDTO
    {
        public MunicipalityUserSignUpInputDTO()
        {

        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int MunicipalityId { get; set; }
    }
}
