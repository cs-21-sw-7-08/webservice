using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.DataAccessLayer;
using wasp.Models;

namespace wasp.Controllers
{
    [ApiController]
    [Route("WASP/Issues/")]
    public class IssueController : ControllerBase
    {
        TestData dummy = new();
        DataService dataService = new();

        [HttpGet(nameof(GetIssueDetails))]
        public async Task<WASPResponse<Issue>> GetIssueDetails(int id)
        {
            var issuedets = await dataService.GetIssueDetails(id);


            return new WASPResponse<Issue>(issuedets);
        }

        [HttpPost(nameof(CreateIssue))]
        public async Task<WASPResponse> CreateIssue(Issue issue)
        {
            Issue formatted = new()
            {
                Id = issue.Id,
                Name = issue.Name,
                Description = issue.Description,
                Status = issue.Status
            };
            await dataService.CreateIssue(formatted);

            return new WASPResponse();

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
                return new WASPResponse(25, "There was no issue to be edited");
            }
            return new WASPResponse<Issue>(updatedIssue);
        }
    }
}
