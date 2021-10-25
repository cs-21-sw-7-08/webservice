using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Models;
using wasp.DataAccessLayer;

namespace wasp.Controllers
{
    [ApiController]
    [Route("WASP/Municipality/[action]")]
    public class MunicipalityController : ControllerBase
    {
        DataService dataService = new();
        
        [HttpPost]
        public async Task<WASPResponse> SignUp(MunicipalUser munUser)
        {
            await dataService.MunicipalSignUp(munUser);
            return new WASPResponse();
        }

        [HttpGet]
        public async Task<WASPResponse> LogIn(MunicipalUser munUser)
        {
            await dataService.MunicipalLogIn(munUser);
            return new WASPResponse();
        }

        [HttpPost]
        public async Task<WASPResponse> CreateResponse(MunicipalResponse response)
        {
            await dataService.CreateResponse(response);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateResponse(MunicipalResponse response)
        {
            await dataService.UpdateResponse(response);
            return new WASPResponse();
        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteResponse(int response_id)
        {
            await dataService.DeleteResponse(response_id);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateIssueStatus(int issue_id)
        {
            await dataService.UpdateIssueStatus(issue_id);
            return new WASPResponse();
        }

    }
}
