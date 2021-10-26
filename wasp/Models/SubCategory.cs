using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
