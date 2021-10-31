using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Interfaces;
using WASP.Models;
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

                var municipalityResponse = await context.MunicipalityResponses
                                                .FirstOrDefaultAsync(x => x.IssueId == response.IssueId && x.MunicipalityUserId == response.MunicipalityUserId);
                // Check if response exist
                if (municipalityResponse != null)
                    return new DataResponse<MunicipalityResponse>((int)ResponseErrors.ResponseDoesNotExist);

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
                    return new DataResponse<MunicipalityResponse>(((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase));

                // Return success response
                return new DataResponse<MunicipalityResponse>(newResponse);
            }
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
