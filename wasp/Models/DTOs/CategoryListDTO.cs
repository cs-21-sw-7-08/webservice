using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class CategoryListDTO
    {
        public CategoryListDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            SubCategories = category.SubCategories.Select(x => new SubCategoryDTO(x)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubCategoryDTO> SubCategories { get; set; }
    }
}
