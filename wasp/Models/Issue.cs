using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

#nullable disable

namespace WASP.Models
{
    public partial class Issue
    {
        public Issue()
        {
            MunicipalityResponses = new HashSet<MunicipalityResponse>();
            Reports = new HashSet<Report>();
            Verifications = new HashSet<Verification>();
        }

        public int Id { get; set; }
        public int CitizenId { get; set; }
        public int MunicipalityId { get; set; }
        public int IssueStateId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public Geometry Location { get; set; }
        public string Address { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }

        public virtual Citizen Citizen { get; set; }
        public virtual IssueState IssueState { get; set; }
        public virtual Municipality Municipality { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<MunicipalityResponse> MunicipalityResponses { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Verification> Verifications { get; set; }
    }
}
