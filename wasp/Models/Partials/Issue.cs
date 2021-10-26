using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WASP.Models;

namespace WASP.Models
{
    public partial class Issue
    {
        [NotMapped]
        [JsonPropertyName(nameof(Location))]
        public Objects.Location LocationPlaceHolder { get => new Objects.Location(Point.Y, Point.X); set => Point = new Point(value.Longitude, value.Latitude); }
        [NotMapped]
        [JsonIgnore]
        public Point Point { get => Location as Point; set => Location = value; }
    }
}
