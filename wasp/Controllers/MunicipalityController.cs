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
using WASP.Models.DTOs;

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
        public async Task<WASPResponse<MunicipalityUserSignUpOutputDTO>> SignUp(MunicipalityUserSignUpInputDTO munUser)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.MunicipalitySignUp(munUser),
                (dataResponse) => new WASPResponse<MunicipalityUserSignUpOutputDTO>(dataResponse.Result)
            );
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
        public async Task<WASPResponse<MunicipalityResponseOutputDTO>> CreateResponse(MunicipalityResponseInputDTO response)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.CreateResponse(response),
               (dataResponse) => new WASPResponse<MunicipalityResponseOutputDTO>(dataResponse.Result)
           );
        }

        [HttpPut]
        public async Task<WASPResponse<MunicipalityResponseOutputDTO>> UpdateResponse(int responseId, IEnumerable<WASPUpdate> updates)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.UpdateResponse(responseId, updates),
               (dataResponse) => new WASPResponse<MunicipalityResponseOutputDTO>(dataResponse.Result)
           );
        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteResponse(int responseId)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.DeleteResponse(responseId),
               (dataResponse) => new WASPResponse()
           );
        }

        [HttpPut]
        public async Task<WASPResponse> UpdateIssueStatus(int issueId)
        {
            //await DataService.UpdateIssueStatus(issue_id);
            return new WASPResponse();
        }

    }
}
