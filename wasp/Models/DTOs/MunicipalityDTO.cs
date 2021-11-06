using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class MunicipalityDTO
    {
        public MunicipalityDTO()
        {

        }
        public MunicipalityDTO(Municipality municipality)
        {
            Id = municipality.Id;
            Name = municipality.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
