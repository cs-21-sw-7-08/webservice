using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wasp.Models
{
    public class WASPResponse
    {
        public bool IsSuccessful { get; set; }
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }

        public WASPResponse()
        {
            IsSuccessful = true;
            ErrorNo = 0;
        }

        public WASPResponse(int errorNo, string errorMessage = null)
        {
            IsSuccessful = false;
            ErrorNo = errorNo;
            ErrorMessage = errorMessage;
        }
    }

    public class WASPResponse<ResultType> : WASPResponse
    {
        public WASPResponse(ResultType result)
        {
            IsSuccessful = true;
            ErrorNo = 0;
            Result = result;
        }

        public WASPResponse(int errorNo, string errorMessage = null) : base(errorNo, errorMessage)
        {
            Result = default;
        }

        public ResultType Result { get; set; }
    }
}
