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
        /// <summary>
        /// Blocks a given citizen. Takes a <paramref name="citizenId"/> and sets their isBlocked status to true
        /// <para>If their status is already blocked, returns an errorcode</para>
        /// </summary>
        /// <returns>
        /// DataResponse with result and potential error code
        /// </returns>
        /// <param name="citizenId"></param>
        /// <returns></returns>
        public async Task<DataResponse> BlockCitizen(int citizenId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                // Get citizen from context
                Citizen citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                // Check if citizen exists; return errorResponse if null
                if (citizen == null)
                    return new DataResponse((int)ResponseErrors.CitizenDoesNotExist);
                // Set flag if not set; return errorResponse if already set
                if (citizen.IsBlocked == false)
                    citizen.IsBlocked = true;
                else
                    return new DataResponse(400, "User is already blocked");
                // Save the changes
                await context.SaveChangesAsync();
                // Return success response
                return new DataResponse();
            }
        }
        /// <summary>
        /// Removes the blocked status from a given citizen. Takes a <paramref name="citizenId"/> and sets their isBlocked status to False
        /// <para>If their status is already unblocked, returns an errorcode</para>
        /// </summary>
        /// <returns>
        /// DataResponse with result and potential error code
        /// </returns>
        /// <param name="citizenId"></param>
        /// <returns></returns>
        public async Task<DataResponse> UnblockCitizen(int citizenId)
        {
            using var context = ContextFactory.CreateDbContext();
            {
                //Get citizen from context
                Citizen citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                // Check if citizen exists; return errorResponse if null
                if (citizen == null)
                    return new DataResponse((int)ResponseErrors.CitizenDoesNotExist);
                // Set flag if not set; return errorResponse if already set
                if (citizen.IsBlocked == true)
                    citizen.IsBlocked = false;
                else
                    //TODO: Add errorcode in ErrorResponse enum
                    return new DataResponse(400, "User is not blocked");
                await context.SaveChangesAsync();
                return new DataResponse();
            }
        }

        public Task<DataResponse<Citizen>> CitizenLogIn(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Citizen>> CitizenSignUp(Citizen citizen)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Removes a citizen from the database. Takes a <paramref name="citizenId"/>, and deletes the citizen with matching ID.
        /// </summary>
        /// <param name="citizenId"></param>
        /// <returns></returns>
        public async Task<DataResponse> DeleteCitizen(int citizenId)
        {
            using var context = ContextFactory.CreateDbContext();
            {
                // Get Citizen
                Citizen citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                // Check if citizen exists; return errorResponse if null
                if (citizen == null)
                    return new DataResponse((int)ResponseErrors.CitizenDoesNotExist);
                // Remove the citizen from context
                context.Remove(citizen);
                // Save changes
                await context.SaveChangesAsync();
                return new DataResponse();
 }
        }

        #endregion
    }
}
