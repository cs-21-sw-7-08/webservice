using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class MunicipalityUserSignUpOutputDTO
    {
        public MunicipalityUserSignUpOutputDTO(MunicipalityUser muniUser)
        {
            Id = muniUser.Id;
        }
        public int Id { get; set; }
    }
}
