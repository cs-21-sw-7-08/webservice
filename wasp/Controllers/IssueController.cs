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
    [Route("[controller]")]
    public class IssueController : ControllerBase
    {
        [HttpGet]
        public async Task<WASPResponse<Issue>> GetIssue()
            {
            var dataService = new DataService();
            var issue = await dataService.GetIssueDetails();


            return new WASPResponse<Issue>(issue);
            }
    }
}
