using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.Objects;

namespace WASP.Interfaces
{
    public interface IDataService
    {

        // Issue Functions        
        public Task<DataResponse<IssueDetailsDTO>> GetIssueDetails(int issueId);
        public Task<DataResponse<IEnumerable<IssuesOverviewDTO>>> GetIssueOverview(IssuesOverviewFilter issueOverviewFilter);
        public Task<DataResponse> CreateIssue(IssueCreateDTO issue);
        public Task<DataResponse> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates);
        public Task<DataResponse> DeleteIssue(int issueId);
        public Task<DataResponse> VerifyIssue(int issueId, int citizenId);
        public Task<DataResponse> ReportIssue(int issueId, int reportCategoryId);
        public Task<DataResponse<IEnumerable<CategoryListDTO>>> GetCategories();
        public Task<DataResponse> UpdateIssueStatus(int issueId, int issueStateId);

        // Municipality Functions
        public Task<DataResponse<IEnumerable<MunicipalityDTO>>> GetMunicipalities();
        public Task<DataResponse<MunicipalityUser>> MunicipalitySignUp(MunicipalityUser muniUser);
        public Task<DataResponse<MunicipalityUserDTO>> MunicipalityLogIn(MunicipalityUserLoginDTO muniUser);
        public Task<DataResponse<MunicipalityResponseDTO>> CreateResponse(MunicipalityResponseDTO response);
        public Task<DataResponse> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates);
        public Task<DataResponse> DeleteResponse(int responseId);

        // User Functions
        public Task<DataResponse<Citizen>> CitizenSignUp(Citizen citizen);
        public Task<DataResponse<CitizenDTO>> CitizenLogIn(CitizenLoginDTO citizen);
        public Task<DataResponse> BlockCitizen(int citizenId);
        public Task<DataResponse> UnblockCitizen(int citizenId);
        public Task<DataResponse> DeleteCitizen(int citizenId);
    }
}
