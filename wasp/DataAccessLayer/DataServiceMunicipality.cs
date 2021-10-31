using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Interfaces;
using WASP.Models;
using WASP.Objects;
using WASP.Utilities;

namespace WASP.DataAccessLayer
{
    public partial class DataService : IDataService
    {
        #region Methods

        public async Task<DataResponse<MunicipalityResponse>> CreateResponse(MunicipalityResponse response)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                // Get issue
                var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == response.IssueId);
                // Check if issue exist
                if (issue == null)
                    return new DataResponse<MunicipalityResponse>((int)ResponseErrors.IssueDoesNotExist);
                // Get municipality user
                var municipalityUser = await context.MunicipalityUsers.FirstOrDefaultAsync(x => x.Id == response.MunicipalityUserId);
                // Check if municipality user exist
                if (municipalityUser == null)
                    return new DataResponse<MunicipalityResponse>((int)ResponseErrors.MunicipalityUserDoesNotExist);
                // Check if municipality user and issue are from the same municipality
                if (issue.MunicipalityId == municipalityUser.MunicipalityId)
                    return new DataResponse<MunicipalityResponse>((int)ResponseErrors.MunicipalityUserMunicipalityIdDoesNotMatchIssueId);

                // Create new issue
                MunicipalityResponse newResponse = new();
                // Update properties
                DataServiceUtil.UpdateProperties(response, newResponse);
                // Set date created
                newResponse.DateCreated = DateTime.Now;

                // Save changes to the database
                var changes = await context.SaveChangesAsync();
                // Check that the number of changed entities is 1
                // as one new Rosponse is added to the database
                if (changes != 1)
                    return new DataResponse<MunicipalityResponse>((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);

                // Return success response
                return new DataResponse<MunicipalityResponse>(newResponse);
            }
        }

        public async Task<DataResponse> DeleteResponse(int responseId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                // Get response
                var response = await context.MunicipalityResponses.FirstOrDefaultAsync(x => x.Id == responseId);
                // Check if response exist
                if (response == null)
                    return new DataResponse((int)ResponseErrors.ResponseDoesNotExist);

                // Remove response
                context.MunicipalityResponses.Remove(response);

                // Save changes in database
                var changes = await context.SaveChangesAsync();

                // Return success response
                return new DataResponse();
            }
        }


        public async Task<DataResponse> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates)
        {
            // Check WASPUpdate list
            if (!DataServiceUtil.CheckWASPUpdateList(updates.ToList(), MunicipalityResponse.GetPropertiesThatAreAllowedToBeUpdated()))
                return new DataResponse((int)ResponseErrors.WASPUpdateListBadFormat);

            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   // Get response
                   var response = await context.MunicipalityResponses.FirstOrDefaultAsync(x => x.Id == responseId);
                   // Check if response exist
                   if (response == null)
                       return new DataResponse(((int)ResponseErrors.ResponseDoesNotExist));
                   // Go through the updates
                   foreach (var update in updates)
                   {
                       // Update property value
                       DataServiceUtil.UpdateProperty(update.Value, update.Name, response);
                   }
                   // Save changes to the database
                   await context.SaveChangesAsync();
                   // Return success response
                   return new DataResponse();
               }
            );
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
