using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace WASP.Controllers
{
    [ApiController]
    [Route("WASP/Municipality/[action]")]
    public class MunicipalityController : BaseController
    {
        public MunicipalityController(IDbContextFactory<HiveContext> contextFactory) : base(contextFactory)
        {

        }


        [HttpPost]
        public async Task<WASPResponse> SignUp(MunicipalityUser munUser)
        {
            await DataService.MunicipalSignUp(munUser);
            return new WASPResponse();
        }

        [HttpGet]
        public async Task<WASPResponse> LogIn(MunicipalityUser munUser)
        {
            await DataService.MunicipalLogIn(munUser);
            return new WASPResponse();
        }

        [HttpPost]
        public async Task<WASPResponse> CreateResponse(MunicipalityResponse response)
        {
            await DataService.CreateResponse(response);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateResponse(MunicipalityResponse response)
        {
            await DataService.UpdateResponse(response);
            return new WASPResponse();
        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteResponse(int response_id)
        {
            await DataService.DeleteResponse(response_id);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateIssueStatus(int issue_id)
        {
            await DataService.UpdateIssueStatus(issue_id);
            return new WASPResponse();
        }

    }
}
