using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class MunicipalityUser
    {
        public MunicipalityUser()
        {
            MunicipalityResponses = new HashSet<MunicipalityResponse>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }
        public virtual ICollection<MunicipalityResponse> MunicipalityResponses { get; set; }
    }
}
