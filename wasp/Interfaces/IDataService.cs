using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.Models.DTOs;
using WASP.Objects;

namespace WASP.Interfaces
{
    public interface IDataService
    {

        // Issue Functions        
        /// <summary>
        /// This function is used when more information on a specific Issue is needed. It takes an issueId and returns an IssueDatailsDTO that contains the details of the given Issue. These Include the state, 
        /// the category and subcategory, the municipality and any municipality responses and the number of verifications.
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        public Task<DataResponse<IssueDetailsDTO>> GetIssueDetails(int issueId);
        /// <summary>
        /// Takes an IssuesOverviewFilter as input and returns a list of IssuesOverviewDTO. IssuesOverviewFilter filters Issues by properties of the issues including
        /// the issue state, the category and subcategory and a given time period. 
        /// </summary>
        /// <param name="issueOverviewFilter"></param>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter issueOverviewFilter);
        /// <summary>
        /// Takes an IssueCreateDTO as input and returns a success or error DataResponse. The IssueCreateDTO needs to contain an int CitizenId, a string Decription, an int MunicipalityId, a subcategoryId and a location.
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public Task<DataResponse> CreateIssue(IssueCreateDTO issue);
        /// <summary>
        /// Takes an int IssueId and a list of WASPUpdate updates and returns a Dataresponse. The updates consists of a Name that identifies attribute being updated and a Value that is the updated attribute value.   
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="updates"></param>
        /// <returns></returns>
        public Task<DataResponse> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates);
        /// <summary>
        /// Takes a int issueId that is then deleted and returns a DataResponse.
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        public Task<DataResponse> DeleteIssue(int issueId);
        /// <summary>
        /// Takes a int issueId and a int citizenId and returns a DataResponse. This function increments the value of verification of the issueId. 
        /// An issue created by a citizen cannot be verified by the same citizen, and it cannot be verified twice by the same person.
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="citizenId"></param>
        /// <returns></returns>
        public Task<DataResponse> VerifyIssue(int issueId, int citizenId);
        /// <summary>
        /// Takes a int issueId and a int reportCategoryId and creates a report on given issue and returns a DataResponse. If a report of the same category exists on the issue then a report TypeCounter is incremented instead.
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="reportCategoryId"></param>
        /// <returns></returns>
        public Task<DataResponse> ReportIssue(int issueId, int reportCategoryId);
        /// <summary>
        /// Returns a list of all the categories.
        /// </summary>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<CategoryListDTO>>> GetCategories();
        /// <summary>
        /// Takes an int issueId and a int issueStateId and returns a DataResponse. This function updates the state of a issue in the datebase. 
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="issueStateId"></param>
        /// <returns></returns>
        public Task<DataResponse> UpdateIssueStatus(int issueId, int issueStateId);

        // Municipality Functions
        /// <summary>
        /// Returns a list of all the registered municipalities.
        /// </summary>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities();
        /// <summary>
        /// Takes a MunicipalityUserSignupInputDTO and returns a MunicipalityUserSignupInputDTO. This function creates a new municipality account in the system.
        /// The MunicipalityUserSignupInputDTO
        /// </summary>
        /// <param name="muniUser"></param>
        /// <returns></returns>
        public Task<DataResponse<MunicipalityUserSignUpOutputDTO>> MunicipalitySignUp(MunicipalityUserSignUpInputDTO muniUser);
        public Task<DataResponse<MunicipalityUserDTO>> MunicipalityLogIn(MunicipalityUserLoginDTO muniUser);
        public Task<DataResponse<MunicipalityResponseOutputDTO>> CreateResponse(MunicipalityResponseInputDTO response);
        public Task<DataResponse<MunicipalityResponseOutputDTO>> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates);
        public Task<DataResponse> DeleteResponse(int responseId);

        // Citizen Functions
        public Task<DataResponse<CitizenDTO>> CitizenSignUp(CitizenSignUpDTO citizen);
        public Task<DataResponse<CitizenDTO>> CitizenLogIn(CitizenLoginDTO citizen);
        public Task<DataResponse> BlockCitizen(int citizenId);
        public Task<DataResponse> UnblockCitizen(int citizenId);
        public Task<DataResponse> DeleteCitizen(int citizenId);
    }
}
