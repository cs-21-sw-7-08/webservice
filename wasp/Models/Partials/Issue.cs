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
        public Objects.Location LocationPlaceHolder
        {
            get => new Objects.Location((Location as Point).Y, (Location as Point).X);
            set => Location = new Point(value.Longitude, value.Latitude) { SRID = 4326 };
        }
    }
}
