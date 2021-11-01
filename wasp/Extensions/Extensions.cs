using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP
{
    public static class Extensions
    {
        public static EnumType ToEnum<EnumType>(this int i)
            where EnumType : Enum
        {
            return (EnumType)Enum.ToObject(typeof(EnumType), i);
        }
    }
}
