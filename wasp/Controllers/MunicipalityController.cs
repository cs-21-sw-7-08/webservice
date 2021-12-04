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
        public async Task<ActionResult<WASPResponse>> SignUpMunicipality(MunicipalityUserSignUpInputDTO munUser)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.MunicipalitySignUp(munUser),
                (dataResponse) => new WASPResponse()
            );
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse<MunicipalityUserDTO>>> LogInMunicipality(MunicipalityUserLoginDTO munUser)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.MunicipalityLogIn(munUser),
                (dataResponse) => new WASPResponse<MunicipalityUserDTO>(dataResponse.Result)
            );
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse>> CreateMunicipalityResponse(MunicipalityResponseInputDTO response)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.CreateResponse(response),
               (dataResponse) => new WASPResponse()
           );
        }

        [HttpPut]
        public async Task<ActionResult<WASPResponse>> UpdateMunicipalityResponse(int responseId, IEnumerable<WASPUpdate> updates)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.UpdateResponse(responseId, updates),
               (dataResponse) => new WASPResponse()
           );
        }

        [HttpDelete]
        public async Task<ActionResult<WASPResponse>> DeleteMunicipalityResponse(int responseId)
        {
            return await ControllerUtil.GetResponse(
               async () => await DataService.DeleteResponse(responseId),
               (dataResponse) => new WASPResponse()
           );
        }

        [HttpGet]
        public async Task<ActionResult<WASPResponse<IEnumerable<MunicipalityDTO>>>> GetListOfMunicipalities()
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.GetMunicipalities(),
                (dataResponse) => new WASPResponse<IEnumerable<MunicipalityDTO>>(dataResponse.Result)
            );
        }
    }
}
