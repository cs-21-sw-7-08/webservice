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
        public Task<bool> VerifyIssue(int issueId);
        public Task<Report> ReportIssue(int issueId);
        public Task<IEnumerable<Category>> GetCategories();

        //Municipality Functions
        public Task<MuniUser> MuniSignUp(MuniUser muniUser);
        public Task<MuniUser> MuniLogIn(MuniUser muniUser);
        public Task<MuniResponse> CreateResponse(MuniResponse response);
        public Task<MuniResponse> UpdateResponse(int responseId);
        public Task<bool> DeleteResponse(int responseId);
        public Task<Issue> UpdateIssueStatus(int issueId);

        //User
        public Task<Citizen> CitSignUp(Citizen citizen);
        public Task<Citizen> CitLogIn(Citizen citizen);
        public Task<bool> BlockUser(int citizenId);
        public Task<bool> UnblockUser(int citizenId);
        public Task<bool> DeleteUser(int citizenId);

    }
}
