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

        [NotMapped]
        [JsonPropertyName(nameof(Location))]
        public Objects.Location LocationPlaceHolder { get => new Objects.Location(Point.Y, Point.X); set => Point = new Point(value.Longitude, value.Latitude); }
        [NotMapped]
        [JsonIgnore]
        public Point Point { get => Location as Point; set => Location = value; }
    }
}
