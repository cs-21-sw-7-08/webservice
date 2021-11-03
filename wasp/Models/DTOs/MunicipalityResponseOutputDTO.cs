using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class MunicipalityResponseOutputDTO
    {
        public MunicipalityResponseOutputDTO(MunicipalityResponse municipalityResponse)
        {
            Id = municipalityResponse.Id;
            IssueId = municipalityResponse.IssueId;
            MunicipalityUserId = municipalityResponse.MunicipalityUserId;
            Response = municipalityResponse.Response;
            DateCreated = municipalityResponse.DateCreated;
        }

        public int Id { get; set; }
        public int IssueId { get; set; }
        public int MunicipalityUserId { get; set; }
        public string Response { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
