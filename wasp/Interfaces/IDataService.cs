using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Models;

namespace wasp.Interfaces
{
    internal interface IDataService
    {

        //Issue Functions
        //TODO : make DALResponse maybe.
        public Task<Issue> GetIssueDetails(int issueId);
        public Task<IEnumerable<Issue>> GetIssueOverview(IssueOverviewFilter x);
        public Task<bool> CreateIssue(Issue issue);
        public Task<Issue> UpdateIssue(Issue issue);
        public Task<bool> DeleteIssue(int issueId);
        public Task VerifyIssue(int issueId);
        public Task<Report> ReportIssue(int issueId);
        public Task<IEnumerable<Category>> GetCategories();

        //Municipality Functions
        public Task<MunicipalUser> MunicipalSignUp(MunicipalUser muniUser);
        public Task<MunicipalUser> MunicipalLogIn(MunicipalUser muniUser);
        public Task<MunicipalResponse> CreateResponse(MunicipalResponse response);
        public Task<MunicipalResponse> UpdateResponse(MunicipalResponse response);
        public Task DeleteResponse(int responseId);
        public Task UpdateIssueStatus(int issueId);

        //User Functions
        public Task<Citizen> CitizenSignUp(Citizen citizen);
        public Task<Citizen> CitizenLogIn(Citizen citizen);
        public Task BlockCitizen(int citizenId);
        public Task UnblockCitizen(int citizenId);
        public Task DeleteCitizen(int citizenId);
    }
}
