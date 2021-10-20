using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Models;

namespace wasp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IssueController : ControllerBase
    {
        [HttpGet]
        public WASPResponse<Issue> GetIssue()
            {
            return new WASPResponse<Issue>(new Issue { Name = "Hul i Vejen",
                Description = "der er et hul i vejen" } 
                );
            }
    }
}
