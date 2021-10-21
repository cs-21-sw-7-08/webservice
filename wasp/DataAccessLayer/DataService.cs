using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Interfaces;
using wasp.Models;
using System.Reflection;

namespace wasp.DataAccessLayer
{
    public class DataService : IDataService
    {
        static TestData testy = new();

        public Task BlockUser(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<Citizen> CitLogIn(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<Citizen> CitSignUp(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task CreateIssue(Issue issue)
            {
            testy.mockdata.Add(issue);

            return Task.CompletedTask;
        }

        public Task<MuniResponse> CreateResponse(MuniResponse response)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteResponse(int responseId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Issue> GetIssueDetails(int issueId)
        {

            Issue issue = testy.mockdata.FirstOrDefault(x => x.Id == issueId);

            return Task.FromResult(issue);
        }

        public Task<IEnumerable<Issue>> GetIssueOverview(IssueOverviewFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<MuniUser> MuniLogIn(MuniUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<MuniUser> MuniSignUp(MuniUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<Report> ReportIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task UnblockUser(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<Issue> UpdateIssue(Issue issue)
        {
            Issue modIssue = testy.mockdata.FirstOrDefault(x => x.Id == issue.Id);
            if (modIssue == null)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(Issue).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(modIssue).ToString() != property.GetValue(issue).ToString())
                    property.SetValue(modIssue, property.GetValue(issue));
            }

            return Task.FromResult(modIssue);
        }

        public Task UpdateIssueStatus(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task<MuniResponse> UpdateResponse(MuniResponse response)
        {
            throw new NotImplementedException();
        }

        public Task VerifyIssue(int issueId)
        {
            throw new NotImplementedException();
        }
    }
}
