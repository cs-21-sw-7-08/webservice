using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace WASP.Models
{
    public partial class MunicipalityResponse
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int MunicipalityUserId { get; set; }
        public string Response { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public virtual Issue Issue { get; set; }
        [JsonIgnore]
        public virtual MunicipalityUser MunicipalityUser { get; set; }
    }
}
