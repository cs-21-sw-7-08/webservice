using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class IssuesOverviewDTO
    {
        public IssuesOverviewDTO()
        {

        }

        public IssuesOverviewDTO(Issue issue)
        {
            Id = issue.Id;
            Description = issue.Description;
            CitizenId = issue.CitizenId;
            DateCreated = issue.DateCreated;
            Location = issue.Location;            
            Municipality = new MunicipalityDTO(issue.Municipality);
            SubCategory = new SubCategoryDTO(issue.SubCategory);
            Category = new CategoryDTO(issue.Category);
            IssueState = new IssueStateDTO(issue.IssueState);                        
        }

        public int Id { get; set; }
        public int CitizenId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public Geometry Location { get; set; }        

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
    }
}
