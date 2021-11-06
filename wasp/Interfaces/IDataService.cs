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
        /// Takes an issueId and returns an IssueDatailsDTO that contains the details of the given Issue. These Include the state, 
        /// the category and subcategory, the municipality and any municipality responses and the number of verifications.
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        public Task<DataResponse<IssueDetailsDTO>> GetIssueDetails(int issueId);
        /// <summary>
        /// Takes an IssuesOverviewFilter as input and returns a list of IssuesOverviewDTO. 
        /// </summary>
        /// <param name="issueOverviewFilter"></param>
        /// <returns></returns>
        public Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter issueOverviewFilter);
        /// <summary>
        /// Takes an IssueCreateDTO as input and 
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public Task<DataResponse> CreateIssue(IssueCreateDTO issue);
        public Task<DataResponse> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates);
        public Task<DataResponse> DeleteIssue(int issueId);
        public Task<DataResponse> VerifyIssue(int issueId, int citizenId);
        public Task<DataResponse> ReportIssue(int issueId, int reportCategoryId);
        public Task<DataResponse<IEnumerable<CategoryListDTO>>> GetCategories();
        public Task<DataResponse> UpdateIssueStatus(int issueId, int issueStateId);

        // Municipality Functions
        public Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities();
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
