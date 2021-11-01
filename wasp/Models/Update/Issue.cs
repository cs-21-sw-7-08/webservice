using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public partial class Issue
    {
        public static List<string> GetPropertiesThatAreAllowedToBeUpdated()
        {
            return new List<string>()
            {
                nameof(Location),
                nameof(MunicipalityId),                
                nameof(SubCategoryId),
                nameof(Description),
                nameof(Picture1),
                nameof(Picture2),
                nameof(Picture3),
            };
        }
    }
}
