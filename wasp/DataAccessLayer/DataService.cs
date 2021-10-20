using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Interfaces;
using wasp.Models;

namespace wasp.DataAccessLayer
{
    public class DataService : IDataService
    {
        public Task BlockUser()
        {
            throw new NotImplementedException();
        }

        public Task CreateIssue()
        {
            throw new NotImplementedException();
        }

        public Task<MuncResponse> CreateResponse()
        {
            throw new NotImplementedException();
        }

        public Task DeleteIssue()
        {
            throw new NotImplementedException();
        }

        public Task DeleteResponse()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Issue> GetIssueDetails()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Issue>> GetIssueOverview()
        {
            throw new NotImplementedException();
        }

        public Task<Citizen> LogIn()
        {
            throw new NotImplementedException();
        }

        public Task<Report> ReportIssue()
        {
            throw new NotImplementedException();
        }

        public Task<Citizen> SignUp()
        {
            throw new NotImplementedException();
        }

        public Task UnblockUser()
        {
            throw new NotImplementedException();
        }

        public Task<Issue> UpdateIssue()
        {
            throw new NotImplementedException();
        }

        public Task UpdateIssueStatus()
        {
            throw new NotImplementedException();
        }

        public Task<MuncResponse> UpdateResponse()
        {
            throw new NotImplementedException();
        }

        public Task VerifyIssue()
        {
            throw new NotImplementedException();
        }
    }
}
