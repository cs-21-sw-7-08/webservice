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

        public async Task<DataResponse<CitizenDTO>> CitizenLogIn(CitizenLoginDTO citizenLogin)
        {
            using (var context = ContextFactory.CreateDbContext())
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
                // TODO: ADD ERROR
                if(citizen == null)
                    return new DataResponse<CitizenDTO>((int)ResponseErrors.CitizenWithTheseCredentialsHasNotBeenSignedUp);
                //Returns success response
                return new DataResponse<CitizenDTO>(new CitizenDTO(citizen));
            }
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
