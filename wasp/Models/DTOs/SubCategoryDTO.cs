using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class SubCategoryDTO
    {
        public SubCategoryDTO(SubCategory subCategory)
        {
            Id = subCategory.Id;
            CategoryId = subCategory.CategoryId;
            Name = subCategory.Name;
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
