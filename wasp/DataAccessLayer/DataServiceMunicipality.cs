using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Interfaces;
using WASP.Models;
using WASP.Models.DTOs;
using WASP.Objects;
using WASP.Utilities;

namespace WASP.DataAccessLayer
{
    public partial class DataService : IDataService
    {
        #region Methods

        public async Task<DataResponse<MunicipalityResponseOutputDTO>> CreateResponse(MunicipalityResponseInputDTO response)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   // Get issue
                   var issue = await context.Issues.FirstOrDefaultAsync(x => x.Id == response.IssueId);
                   // Check if issue exist
                   if (issue == null)
                       return new DataResponse<MunicipalityResponseOutputDTO>((int)ResponseErrors.IssueDoesNotExist);
                   // Get municipality user
                   var muniUser = await context.MunicipalityUsers.FirstOrDefaultAsync(x => x.Id == response.MunicipalityUserId);
                   // Check if municipalityId corrospond between the issue and the municipality user
                   if (issue.MunicipalityId != muniUser.MunicipalityId)
                       return new DataResponse<MunicipalityResponseOutputDTO>((int)ResponseErrors.MunicipalityUserMunicipalityIdDoesNotMatchIssueMunicipalityId);

                   // Create new issue
                   MunicipalityResponse newResponse = new();
                   // Update properties
                   DataServiceUtil.UpdateProperties(response, newResponse);
                   // Set date created
                   newResponse.DateCreated = DateTime.Now;

                   // Add new response
                   await context.MunicipalityResponses.AddAsync(newResponse);

                   // Save changes to the database
                   var changes = await context.SaveChangesAsync();
                   // Check that the number of changed entities is 1
                   // as one new Rosponse is added to the database
                   if (changes != 1)
                       return new DataResponse<MunicipalityResponseOutputDTO>((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);

                   // Return success response
                   return new DataResponse<MunicipalityResponseOutputDTO>(new MunicipalityResponseOutputDTO(newResponse));
               }
            );
        }

        public async Task<DataResponse> DeleteResponse(int responseId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
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
                   // Check that the number of changed entities is 1
                   // as one Rosponse is deleted from the database
                   if (changes != 1)
                       return new DataResponse((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);

                   // Return success response
                   return new DataResponse();
               }
            );
        }


        public async Task<DataResponse<MunicipalityResponseOutputDTO>> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates)
        {
            // Check WASPUpdate list
            if (!DataServiceUtil.CheckWASPUpdateList(updates.ToList(), MunicipalityResponse.GetPropertiesThatAreAllowedToBeUpdated()))
                return new DataResponse<MunicipalityResponseOutputDTO>((int)ResponseErrors.WASPUpdateListBadFormat);

            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   // Get response
                   var response = await context.MunicipalityResponses.FirstOrDefaultAsync(x => x.Id == responseId);
                   // Check if response exist
                   if (response == null)
                       return new DataResponse<MunicipalityResponseOutputDTO>((int)ResponseErrors.ResponseDoesNotExist);
                   // Go through the updates
                   foreach (var update in updates)
                   {
                       // Update property value
                       DataServiceUtil.UpdateProperty(update.Value, update.Name, response);
                   }
                   // Set DateEdited to the current time
                   response.DateEdited = DateTime.Now;
                   // Save changes to the database
                   var changes = await context.SaveChangesAsync();
                   // Check that the number of changed entities is 1
                   // as one Rosponse is changed in the database
                   if (changes != 1)
                       return new DataResponse<MunicipalityResponseOutputDTO>((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);
                   // Return success response
                   return new DataResponse<MunicipalityResponseOutputDTO>(new MunicipalityResponseOutputDTO(response));
               }
            );
        }

        public async Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities()
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   var municipalities = await context.Municipalities
                       .AsNoTracking()
                       .Select(municipality => new MunicipalityDTO(municipality))
                       .ToListAsync();
                   return new DataResponse<IEnumerable<MunicipalityDTO>>(municipalities);
               }
               );
        }

        public async Task<DataResponse<MunicipalityUserDTO>> MunicipalityLogIn(MunicipalityUserLoginDTO muniUser)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
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
               );
        }

        public async Task<DataResponse<MunicipalityUserSignUpOutputDTO>> MunicipalitySignUp(MunicipalityUserSignUpInputDTO muniUser)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   // Check if there already exists an user with the emailed used
                   var user = await context.MunicipalityUsers.FirstOrDefaultAsync(x => x.Email == muniUser.Email);
                   if (user != null)
                       return new DataResponse<MunicipalityUserSignUpOutputDTO>((int)ResponseErrors.MunicipalityUserSignUpEmailIsAlreadyUsed);

                   // Check if the municipalityId exist
                   var municipality = await context.Municipalities.FirstOrDefaultAsync(x => x.Id == muniUser.MunicipalityId);
                   if (municipality == null)
                       return new DataResponse<MunicipalityUserSignUpOutputDTO>((int)ResponseErrors.MunicipalityDoesNotExist);

                   // Create new municipality user
                   MunicipalityUser newUser = new();
                   // Update properties
                   DataServiceUtil.UpdateProperties(muniUser, newUser);

                   // Add new user
                   await context.MunicipalityUsers.AddAsync(newUser);

                   // Save changes to the database
                   var changes = await context.SaveChangesAsync();
                   // Check that the number of changed entities is 1
                   // as one new municipality user is added to the database
                   if (changes != 1)
                       return new DataResponse<MunicipalityUserSignUpOutputDTO>((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);

                   // Return success response
                   return new DataResponse<MunicipalityUserSignUpOutputDTO>(new MunicipalityUserSignUpOutputDTO(newUser));
               }
            );
        }

        #endregion
    }
}
