using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class IssueCreateDTO
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
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }

        [JsonPropertyName(nameof(Location))]
        public Objects.Location LocationPlaceHolder
        {
            get => new Objects.Location((Location as Point).Y, (Location as Point).X);
            set => Location = new Point(value.Longitude, value.Latitude) { SRID = 4326 };
        }
    }
}
