using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Objects
{
    public class WASPUpdate
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not WASPUpdate waspUpdate)
                return false;
            return Name == waspUpdate.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
