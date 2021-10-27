using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Interfaces;
using WASP.Models;

namespace WASP.DataAccessLayer
{
    public partial class DataService : IDataService
    {
        #region Methods

        public Task<DataResponse<MunicipalityResponse>> CreateResponse(MunicipalityResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> DeleteResponse(int responseId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<MunicipalityResponse>> UpdateResponse(MunicipalityResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities()
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<MunicipalityUser>> MunicipalityLogIn(MunicipalityUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<MunicipalityUser>> MunicipalitySignUp(MunicipalityUser muniUser)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
