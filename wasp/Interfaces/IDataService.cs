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
        public Task CreateIssue(Issue issue);
        public Task<Issue> UpdateIssue(Issue issue);
        public Task DeleteIssue(int issueId);
        public Task VerifyIssue(int issueId);
        public Task<Report> ReportIssue(int issueId);
        public Task<IEnumerable<Category>> GetCategories();

        //Municipality Functions
        public Task<MuniUser> MuniSignUp(MuniUser muniUser);
        public Task<MuniUser> MuniLogIn(MuniUser muniUser);
        public Task<MuniResponse> CreateResponse(MuniResponse response);
        public Task<MuniResponse> UpdateResponse(MuniResponse response);
        public Task DeleteResponse(int responseId);
        public Task UpdateIssueStatus(int issueId);

        //User
        public Task<Citizen> CitSignUp(Citizen citizen);
        public Task<Citizen> CitLogIn(Citizen citizen);
        public Task BlockUser(int citizenId);
        public Task UnblockUser(int citizenId);
        public Task DeleteUser(int citizenId);

    }
}
