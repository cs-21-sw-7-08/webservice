using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class IssueDetailsDTO
    {
        public IssueDetailsDTO(Issue issue)
        {
            Id = issue.Id;
            Description = issue.Description;
            CitizenId = issue.CitizenId;
            DateCreated = issue.DateCreated;
            DateEdited = issue.DateEdited;
            Location = issue.Location;
            Address = issue.Address;
            Picture1 = issue.Picture1;
            Picture2 = issue.Picture2;
            Picture3 = issue.Picture3;
            Municipality = new MunicipalityDTO(issue.Municipality);
            SubCategory = new SubCategoryDTO(issue.SubCategory);
            Category = new CategoryDTO(issue.Category);                        
            IssueState = new IssueStateDTO(issue.IssueState);
            MunicipalityResponses = issue.MunicipalityResponses.Select(x => new MunicipalityResponseOutputDTO(x)).ToList();
            IssueVerificationCitizenIds = issue.Verifications.Select(x => x.CitizenId).ToList();
        }

        public int Id { get; set; }
        public int CitizenId { get; set; }                        
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        [JsonIgnore]
        public Geometry Location { get; set; }
        public string Address { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }

        [JsonPropertyName(nameof(Location))]
        public Objects.Location LocationPlaceHolder
        {
            get => new Objects.Location((Location as Point).Y, (Location as Point).X);
            set => Location = new Point(value.Longitude, value.Latitude) { SRID = 4326 };
        }

        public virtual CategoryDTO Category { get; set; }
        public virtual SubCategoryDTO SubCategory { get; set; }
        public virtual MunicipalityDTO Municipality { get; set; }
        public virtual IssueStateDTO IssueState { get; set; }
        public virtual ICollection<MunicipalityResponseOutputDTO> MunicipalityResponses { get; set; }
        public virtual ICollection<int> IssueVerificationCitizenIds { get; set; }
    }
}
