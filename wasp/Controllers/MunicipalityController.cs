using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasp.Models;
using wasp.DataAccessLayer;
using wasp.Interfaces;

namespace wasp.Controllers
{
    [ApiController]
    [Route("WASP/Municipality/[action]")]
    public class MunicipalityController : ControllerBase
    {
        DataService dataService = new(); 

        [HttpPost]
        public async Task<WASPResponse> MunicipalitySignUp()
        {
            MuniUser user = new();
            await dataService.MuniSignUp(user);
            return new WASPResponse();
        }

        [HttpGet]
        public async Task<WASPResponse> MunicipalityLogIn()
        {
            MuniUser user = new();
            await dataService.MuniLogIn(user);
            return new WASPResponse();
        }

        [HttpPost]
        public async Task<WASPResponse> CreateMunicipalityResponse(MuniResponse response)
        {
            await dataService.CreateResponse(response);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateMunicipalityResponse(int response_id)
        {
            await dataService.UpdateResponse(response_id);
            return new WASPResponse();
        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteMunicipalityResponse(int response_id)
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
