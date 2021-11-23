using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class Citizen
    {
        public Citizen()
        {
            Issues = new HashSet<Issue>();
            Verifications = new HashSet<Verification>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
        public int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<Verification> Verifications { get; set; }
    }
}
