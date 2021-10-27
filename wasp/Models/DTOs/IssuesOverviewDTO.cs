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
            MunicipalityId = issue.MunicipalityId;
            CategoryId = issue.CategoryId;
            CitizenId = issue.CitizenId;
            DateCreated = issue.DateCreated;
            IssueStateId = issue.IssueStateId;
            Location = issue.Location;
            SubCategoryId = issue.SubCategoryId;
        }

        public int Id { get; set; }
        public int CitizenId { get; set; }
        public int MunicipalityId { get; set; }
        public int IssueStateId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
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
    }
}
