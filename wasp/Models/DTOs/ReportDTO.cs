using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class ReportDTO
    {
        public ReportDTO(Report report)
        {
            Id = report.Id;
            IssueId = report.IssueId;
            ReportCategory = new ReportCategoryDTO(report.ReportCategory);
            TypeCounter = report.TypeCounter;
        }

        public int Id { get; set; }
        public int IssueId { get; set; }
        public int ReportCategoryId { get; set; }
        public int TypeCounter { get; set; }
        public ReportCategoryDTO ReportCategory {get; set;}
}
}
