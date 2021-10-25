using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wasp.Models
{
    public class DataResponse
    {
        public bool IsSuccessful { get; set; }
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }

        public DataResponse()
        {
            IsSuccessful = true;
            ErrorNo = 0;
        }

        public DataResponse(int errorNo, string errorMessage = null)
        {
            IsSuccessful = false;
            ErrorNo = errorNo;
            ErrorMessage = errorMessage;
        }
    }

    public class DataResponse<ResultType> : DataResponse
    {

        public ResultType Result { get; set; }

        public DataResponse(ResultType result)
        {
            IsSuccessful = true;
            ErrorNo = 0;
            Result = result;
        }

        public DataResponse(int errorNo, string errorMessage = null) : base(errorNo, errorMessage)
        {
            Result = default;
        }

    }
}
