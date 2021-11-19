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

        public async Task<DataResponse> BlockCitizen(int citizenId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
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
                       return new DataResponse((int)ResponseErrors.CitizenAlreadyBlocked);
                   // Save the changes
                   await context.SaveChangesAsync();
                   // Return success response
                   return new DataResponse();
               }
               );
        }

        public async Task<DataResponse> UnblockCitizen(int citizenId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
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
                       return new DataResponse((int)ResponseErrors.CitizenAlreadyUnblocked);
                   await context.SaveChangesAsync();
                   return new DataResponse();
               }
               );
        }

        public async Task<DataResponse<CitizenDTO>> CitizenLogIn(CitizenLoginDTO citizenLogin)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   Citizen citizen = null;
                   //case if PhoneNo is given
                   if (citizenLogin.Email == null && citizenLogin.PhoneNo != null)
                   {
                       citizen = await context.Citizens
                           .AsNoTracking()
                           .FirstOrDefaultAsync(x => x.PhoneNo == citizenLogin.PhoneNo);
                   }
                   //case if Email is given
                   else if (citizenLogin.Email != null && citizenLogin.PhoneNo == null)
                   {
                       citizen = await context.Citizens
                           .AsNoTracking()
                           .FirstOrDefaultAsync(x => x.Email.ToLower() == citizenLogin.Email.ToLower());
                   }
                   //if both email and phone number is null or if they are both not null an error is thrown
                   else
                   {
                       return new DataResponse<CitizenDTO>((int)ResponseErrors.CitizenLoginBothEmailAndPhoneNumberCannotBeFilled);
                   }
                   //If the email or phoneNo has not been signed up yet
                   if (citizen == null)
                       return new DataResponse<CitizenDTO>((int)ResponseErrors.CitizenWithTheseCredentialsHasNotBeenSignedUp);
                   //Returns success response
                   return new DataResponse<CitizenDTO>(new CitizenDTO(citizen));
               }
               );
        }

        public async Task<DataResponse<CitizenDTO>> CitizenSignUp(CitizenSignUpInputDTO citizen)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   //In case of PhoneNo given then check no citizen with the phoneNo already exist
                   if (citizen.Email == null && citizen.PhoneNo != null)
                   {
                       var phoneNo = await context.Citizens.FirstOrDefaultAsync(x => x.PhoneNo == citizen.PhoneNo);
                       if (phoneNo != null)
                           return new DataResponse<CitizenDTO>((int)ResponseErrors.CitizenSignUpPhoneNoIsAlreadyUsed);
                   }
                   //In case of Email given then check no citizen with the email already exist
                   else if (citizen.Email != null && citizen.PhoneNo == null)
                   {
                       var email = await context.Citizens.FirstOrDefaultAsync(x => x.Email == citizen.Email);
                       if (email != null)
                           return new DataResponse<CitizenDTO>((int)ResponseErrors.CitizenSignUpEmailIsAlreadyUsed);
                   }
                   //In case of invalid parameters, such as both phoneNo and Email not being null
                   else
                       return new DataResponse<CitizenDTO>((int)ResponseErrors.CitizenSignUpInvalidParameters);

                   // Create new Citizen
                   Citizen newCitizen = new();
                   // Update properties
                   DataServiceUtil.UpdateProperties(citizen, newCitizen);
                   // Set isBlocked
                   newCitizen.IsBlocked = false;

                   // Add new citizen
                   await context.Citizens.AddAsync(newCitizen);

                   // Save changes to the database
                   var changes = await context.SaveChangesAsync();
                   // Check that the number of changed entities is 1
                   if (changes != 1)
                       return new DataResponse<CitizenDTO>((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);

                   // Return success response
                   return new DataResponse<CitizenDTO>(new CitizenDTO(newCitizen));
               }
            );
        }

        public async Task<DataResponse> UpdateCitizen(int citizenId, IEnumerable<WASPUpdate> citUpdate)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
                async (context) =>
                {
                    // Check WASPUpdate list for permissable changes
                    if (!DataServiceUtil.CheckWASPUpdateList(citUpdate.ToList(), Citizen.GetPropertiesThatAreAllowedToBeUpdated()))
                        return new DataResponse((int)ResponseErrors.WASPUpdateListBadFormat);

                    //Find the citizen with the given Id
                    Citizen citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == citizenId);
                    if (citizen == null)
                        return new DataResponse((int)ResponseErrors.CitizenDoesNotExist);

                    // Update all WASPUpdate properties in the citizen
                    foreach (WASPUpdate property in citUpdate)
                    {
                        DataServiceUtil.UpdateProperty(property.Value, property.Name, citizen);
                    }

                    // Save changes to the database
                    var changes = await context.SaveChangesAsync();
                    // Check that the number of changed entities is 1
                    if (changes != 1)
                        return new DataResponse<CitizenDTO>((int)ResponseErrors.ChangesCouldNotBeAppliedToTheDatabase);

                    // Return success response
                    return new DataResponse<CitizenDTO>(new CitizenDTO(citizen));
                }
            );
        }

        public async Task<DataResponse> DeleteCitizen(int citizenId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
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
               );
        }
        public async Task<DataResponse<bool>> IsBlockedCitizen(int citizenId)
        {
            return await DataServiceUtil.GetResponse(ContextFactory,
               async (context) =>
               {
                   // Get Citizen
                   Citizen citizen = await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId);
                   // Check if citizen exists; return errorResponse if null
                   if (citizen == null)
                       return new DataResponse<bool>((int)ResponseErrors.CitizenDoesNotExist);
                   //Return true or false to blocked
                   return new DataResponse<bool>(citizen.IsBlocked);
               }
               );
        }
        #endregion
    }
}
