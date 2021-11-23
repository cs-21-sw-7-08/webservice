using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class Municipality
    {
        public Municipality()
        {
            Citizens = new HashSet<Citizen>();
            Issues = new HashSet<Issue>();
            MunicipalityUsers = new HashSet<MunicipalityUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Citizen> Citizens { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<MunicipalityUser> MunicipalityUsers { get; set; }
    }
}
