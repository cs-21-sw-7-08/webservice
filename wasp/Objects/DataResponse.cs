using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class DataResponse
    {
        public bool IsSuccessful => ErrorNo == 0;
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }

        public DataResponse()
        {
            ErrorNo = 0;
        }

        public DataResponse(int errorNo, string errorMessage = null)
        {
            ErrorNo = errorNo;
            ErrorMessage = errorMessage;
        }
    }

    public class DataResponse<ResultType> : DataResponse
    {
        private CitizenDTO result;

        public ResultType Result { get; set; }

        public DataResponse(ResultType result)
        {
            ErrorNo = 0;
            Result = result;
        }

        public DataResponse(int errorNo, string errorMessage = null) : base(errorNo, errorMessage)
        {
            Result = default;
        }
    }
}
