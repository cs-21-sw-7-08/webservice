using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class IssueState
    {
        public IssueState()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
