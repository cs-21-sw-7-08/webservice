using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class Citizen
    {
        public Citizen()
        {
            IssueVerifications = new HashSet<IssueVerification>();
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<IssueVerification> IssueVerifications { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
