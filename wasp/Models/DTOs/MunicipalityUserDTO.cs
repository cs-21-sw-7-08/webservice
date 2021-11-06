using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class MunicipalityUserDTO
    {
        public MunicipalityUserDTO()
        {
                
        }

        public MunicipalityUserDTO(MunicipalityUser municipalityUser)
        {
            Id = municipalityUser.Id;
            Name = municipalityUser.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

}
