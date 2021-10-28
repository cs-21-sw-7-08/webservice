using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Enums;
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

        public async Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities()
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                var municipalities = await context.Municipalities
                    .AsNoTracking()
                    .Select(municipality => new MunicipalityDTO(municipality))
                    .ToListAsync();
                return new DataResponse<IEnumerable<MunicipalityDTO>>(municipalities);
            }
        }

        public async Task<DataResponse<MunicipalityUserDTO>> MunicipalityLogIn(MunicipalityUserLoginDTO muniUser)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                var municipalityUser = await context.MunicipalityUsers
                    .AsNoTracking()
                    //Finds the correct Municipality User from Email and Password. Ignores capital letters
                    .FirstOrDefaultAsync(x => x.Password == muniUser.Password && x.Email.ToLower() == muniUser.Email.ToLower());
                //If email and or password not matched return error
                if (municipalityUser == null)
                    return new DataResponse<MunicipalityUserDTO>((int)ResponseErrors.MunicipalityUserEmailAndOrPasswordNotMatched);
                //Returns success response
                return new DataResponse<MunicipalityUserDTO>(new MunicipalityUserDTO(municipalityUser));
            }
        }

        public Task<DataResponse<MunicipalityUser>> MunicipalitySignUp(MunicipalityUser muniUser)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
