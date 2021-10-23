using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.DataAccessLayer;
using wasp.Interfaces;
using wasp.Models;

namespace wasp.Controllers
{
    [ApiController]
    [Route("WASP/Issues/[action]")]
    public class IssueController : ControllerBase
    {
        DataService dataService = new();

        [HttpGet]
        public async Task<WASPResponse<Issue>> GetIssueDetails(int id)
        {
            var issuedets = await dataService.GetIssueDetails(id);


            return new WASPResponse<Issue>(issuedets);
        }

        [HttpGet]
        public async Task<WASPResponse<IEnumerable<Issue>>> GetListOfIssues()
        {
            IssueOverviewFilter filter = new();
            IEnumerable<Issue> issueList = await dataService.GetIssueOverview(filter);
            return (WASPResponse<IEnumerable<Issue>>)issueList;
        }



        [HttpPost]
        public async Task<WASPResponse> CreateIssue(Issue issue)
        {
            Issue formatted = new()
            {
                Id = issue.Id,
                Name = issue.Name,
                Description = issue.Description,
                Status = issue.Status
            };
            bool waitResult = await dataService.CreateIssue(formatted);

            if (waitResult)
                return new WASPResponse();
            else
                return new WASPResponse(500);

        }

        [HttpPut]
        public async Task<WASPResponse> UpdateIssue(Issue issue)
        {
            Issue updatedIssue;
            try
            {
                updatedIssue = await dataService.UpdateIssue(issue);
            }
            catch (NullReferenceException)
            {
                return new WASPResponse(500, "There was no issue to be edited");
            }
            return new WASPResponse<Issue>(updatedIssue);
        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteIssue(int issue_id)
        {
            bool delResult = await dataService.DeleteIssue(issue_id);

            if (delResult)
            {
                return new WASPResponse();
            }
            else
            {
                return new WASPResponse(500);
            }
        }

        [HttpPut]
        public async Task<WASPResponse> VerifyIssue(int issue_id)
        {
            await dataService.VerifyIssue(issue_id);
            return new WASPResponse();
        }
        
        [HttpPost]
        public async Task<WASPResponse> ReportIssue(int issue_id)
        {
            await dataService.ReportIssue(issue_id);
            return new WASPResponse();
        }

        [HttpGet]
        public async Task<WASPResponse> GetCategories()
        {
            var categoryList = await dataService.GetCategories();
            if (categoryList != null)
            {
                return new WASPResponse<IEnumerable<Category>>(categoryList);
            }
            else
            {
                return new WASPResponse(500);
            }
        }
    }
}
