using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int ReportCategoryId { get; set; }
        public int TypeCounter { get; set; }

        public virtual Issue Issue { get; set; }
        public virtual ReportCategory ReportCategory { get; set; }
    }
}
