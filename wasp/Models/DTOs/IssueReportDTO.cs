using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class IssueReportDTO
    {
        public IssueReportDTO(Issue issue)
        {
            Id = issue.Id;
            MunicipalityId = issue.MunicipalityId;
            Description = issue.Description;
            Citizen = new CitizenDTO(issue.Citizen);
            Reports = issue.Reports.Select(x => new ReportDTO(x)).ToList();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int MunicipalityId { get; set; }
        public CitizenDTO Citizen { get; set; }
        public virtual List<ReportDTO> Reports { get; set; }
    }
}
