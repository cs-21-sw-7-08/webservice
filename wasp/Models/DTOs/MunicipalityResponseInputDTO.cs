using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class MunicipalityResponseInputDTO
    {
        public MunicipalityResponseInputDTO()
        {

        }
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int MunicipalityUserId { get; set; }
        public string Response { get; set; }
    }
}
