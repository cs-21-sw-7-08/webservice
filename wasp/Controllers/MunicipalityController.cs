using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WASP.Utilities;
using WASP.Objects;

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
            await DataService.MunicipalitySignUp(munUser);
            return new WASPResponse();
        }

        [HttpPost]
        public async Task<WASPResponse<MunicipalityUserDTO>> LogIn(MunicipalityUserLoginDTO munUser)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.MunicipalityLogIn(munUser),
                (dataResponse) => new WASPResponse<MunicipalityUserDTO>(dataResponse.Result)
            );
        }

        [HttpPost]
        public async Task<WASPResponse> CreateResponse(MunicipalityResponse response)
        {
            await DataService.CreateResponse(response);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.UpdateIssue(responseId, updates),
               (dataResponse) => new WASPResponse()
           );
        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteResponse(int responseId)
        {
            await DataService.DeleteResponse(responseId);
            return new WASPResponse();
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateIssueStatus(int issueId)
        {
            //await DataService.UpdateIssueStatus(issue_id);
            return new WASPResponse();
        }

    }
}
