using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class ReportCategory
    {
        public ReportCategory()
        {
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
