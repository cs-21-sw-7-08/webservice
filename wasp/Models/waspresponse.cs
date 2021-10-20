using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wasp.Models
{
    public class WASPResponse
    {
        public bool isSuccessful { get; set; }
        public int ErrorNo { get; set; }
    }

    public class WASPResponse<ResultType> : WASPResponse
    {
        public ResultType Result { get; set; }
    }
}
