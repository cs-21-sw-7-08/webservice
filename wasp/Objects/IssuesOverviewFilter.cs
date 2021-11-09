using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Objects
{
    public class IssuesOverviewFilter
    {
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public int? MunicipalityId { get; set; }
        public int? IssueStateId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
