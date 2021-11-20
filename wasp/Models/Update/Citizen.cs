using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public partial class Citizen
    {
        public static List<string> GetPropertiesThatAreAllowedToBeUpdated()
        {
            return new List<string>()
            {
                nameof(Name),
                nameof(Email),
                nameof(PhoneNo)
            };
        }
    }
}
