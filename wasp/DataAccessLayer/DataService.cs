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

        public Task BlockCitizen(int citizenId)
        {
            throw new NotImplementedException();
        }

        public Task<Citizen> CitizenLogIn(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<Citizen> CitizenSignUp(Citizen citizen)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateIssue(Issue issue)
            {
            testy.mockdata.Add(issue);
            if (testy.mockdata.Last().Id == issue.Id)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

        public Task<MunicipalResponse> CreateResponse(MunicipalResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteResponse(int responseId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCitizen(int citizenId)
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

        public Task<MunicipalUser> MunicipalLogIn(MunicipalUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<MunicipalUser> MunicipalSignUp(MunicipalUser muniUser)
        {
            throw new NotImplementedException();
        }

        public Task<Report> ReportIssue(int issueId)
        {
            throw new NotImplementedException();
        }

        public Task UnblockCitizen(int citizenId)
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

        public Task<MunicipalResponse> UpdateResponse(MunicipalResponse response)
        {
            throw new NotImplementedException();
        }

        public Task VerifyIssue(int issueId)
        {
            throw new NotImplementedException();
        }
    }
}
