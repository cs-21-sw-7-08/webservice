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

        public async Task<DataResponse> BlockCitizen(int citizenId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                // Get citizen
                Citizen citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                // Make error checks
                if (citizen == null)
                    return new DataResponse((int)ResponseErrors.CitizenDoesNotExist);
                // Set flag
                citizen.IsBlocked = true;
                // Save the changes
                await context.SaveChangesAsync();
                // Return success response
                return new DataResponse();
            }
        }

        public Task<DataResponse> UnblockCitizen(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Citizen>> CitizenLogIn(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Citizen>> CitizenSignUp(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> DeleteCitizen(int citizenId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
