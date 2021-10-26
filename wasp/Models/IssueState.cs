using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
