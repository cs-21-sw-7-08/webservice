using System;
using System.Collections.Generic;

#nullable disable

namespace WASP.Models
{
    public partial class Verification
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int CitizenId { get; set; }

        public virtual Citizen Citizen { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
