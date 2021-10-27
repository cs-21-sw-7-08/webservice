using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class CategoryDTO
    {
        public CategoryDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
