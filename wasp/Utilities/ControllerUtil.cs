using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Models;

namespace WASP.Utilities
{
    public class ControllerUtil
    {
        /// <summary>
        /// Method used to get WASPResponse object
        /// </summary>
        /// <typeparam name="TypeDataResponse">DataResponse type</typeparam>
        /// <typeparam name="TypeWASPResponse">WASPResponse type</typeparam>
        /// <param name="getDataResponseMethod">Method returning DataResponse object</param>
        /// <param name="parseDataResponseMethod">Method returning WASPResponse object</param>
        /// <returns></returns>
        public static async Task<TypeWASPResponse> GetResponse<TypeDataResponse, TypeWASPResponse>
            (
                Func<Task<TypeDataResponse>> getDataResponseMethod, 
                Func<TypeDataResponse, TypeWASPResponse> parseDataResponseMethod
            )
            where TypeDataResponse : DataResponse
            where TypeWASPResponse : WASPResponse
        {
            TypeWASPResponse getErrorResponse(int errorNo, string errorMessage)
            {
                Type type = typeof(TypeWASPResponse);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(int), typeof(string) });
                object instance = ctor.Invoke(new object[] { errorNo, errorMessage });
                return (TypeWASPResponse)instance;
            }

            try
            {
                // Get data
                var dataResponse = await getDataResponseMethod();
                // Check for errors
                if (!dataResponse.IsSuccessful)
                    return getErrorResponse(dataResponse.ErrorNo, dataResponse.ErrorMessage);
                // Return response
                return parseDataResponseMethod(dataResponse);
            }
            catch (Exception exc)
            {
                return getErrorResponse((int)ResponseErrors.AnExceptionOccurredInAController, exc.Message);
            }
        }
    }
}
