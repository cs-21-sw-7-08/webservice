using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WASP.DataAccessLayer;
using WASP.Enums;
using WASP.Models;
using WASP.Objects;
using WASP.Utilities;

namespace WASP.Controllers
{
    [ApiController]
    [Route("WASP/Issues/[action]")]
    public class IssueController : BaseController
    {
        public IssueController(IDbContextFactory<HiveContext> contextFactory) : base(contextFactory)
        {

        }


        [HttpGet]
        public async Task<ActionResult<WASPResponse<IssueDetailsDTO>>> GetIssueDetails(int issueId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.GetIssueDetails(issueId),
                (dataResponse) => new WASPResponse<IssueDetailsDTO>(dataResponse.Result)
            );
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse<IEnumerable<IssuesOverviewDTO>>>> GetListOfIssues(IssuesOverviewFilter filter)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.GetIssueOverview(filter),
                (dataResponse) => new WASPResponse<IEnumerable<IssuesOverviewDTO>>(dataResponse.Result)
            );
        }

        [HttpGet]
        public async Task<ActionResult<WASPResponse<IEnumerable<CategoryListDTO>>>> GetListOfCategories()
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.GetCategories(),
                (dataResponse) => new WASPResponse<IEnumerable<CategoryListDTO>>(dataResponse.Result)
            );
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse>> CreateIssue(IssueCreateDTO issue)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.CreateIssue(issue),
                (dataResponse) => new WASPResponse()
            );
        }

        [HttpPut]
        public async Task<ActionResult<WASPResponse>> UpdateIssue(int issueId, IEnumerable<WASPUpdate> updates)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.UpdateIssue(issueId, updates),
                (dataResponse) => new WASPResponse()
            );
        }

        [HttpDelete]
        public async Task<ActionResult<WASPResponse>> DeleteIssue(int issueId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.DeleteIssue(issueId),
                (dataResponse) => new WASPResponse()
            );
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse>> VerifyIssue(int issueId, int citizenId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.VerifyIssue(issueId, citizenId),
                (dataResponse) => new WASPResponse()
            );
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse>> ReportIssue(int issueId, int reportCategoryId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.ReportIssue(issueId, reportCategoryId),
                (dataResponse) => new WASPResponse()
            );
        }
    }
}
