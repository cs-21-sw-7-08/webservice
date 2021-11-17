using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models.DTOs
{
    public class ReportCategoryDTO
    {
        public ReportCategoryDTO()
        {

        }
        public ReportCategoryDTO(ReportCategory reportCategory)
        {
            Id = reportCategory.Id;
            Name = reportCategory.Name;

        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
