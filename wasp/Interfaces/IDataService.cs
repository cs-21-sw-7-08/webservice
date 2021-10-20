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
        public Task<Issue> GetIssueDetails();
        public Task<IEnumerable<Issue>> GetIssueOverview();
        public Task CreateIssue();
        public Task<Issue> UpdateIssue();
        public Task DeleteIssue();
        public Task VerifyIssue();
        public Task<Report> ReportIssue();

        //Municipality Functions
        public Task<MuncResponse> CreateResponse();
        public Task<MuncResponse> UpdateResponse();
        public Task DeleteResponse();
        public Task UpdateIssueStatus();

        //User
        public Task<Citizen> SignUp();
        public Task<Citizen> LogIn();
        public Task BlockUser();
        public Task UnblockUser();
        public Task DeleteUser();

        //
        public Task<IEnumerable<Category>> GetCategories();

    }
}
