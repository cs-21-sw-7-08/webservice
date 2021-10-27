using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class WASPResponse
    {
        public bool IsSuccessful => ErrorNo == 0;
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }

        public WASPResponse()
        {
            ErrorNo = 0;
        }

        public WASPResponse(int errorNo, string errorMessage = null)
        {
            ErrorNo = errorNo;
            ErrorMessage = errorMessage;
        }
    }

    public class WASPResponse<ResultType> : WASPResponse
    {
        public ResultType Result { get; set; }

        public WASPResponse(ResultType result)
        {
            ErrorNo = 0;
            Result = result;
        }

        public WASPResponse(int errorNo, string errorMessage = null) : base(errorNo, errorMessage)
        {
            Result = default;
        }
    }
}
