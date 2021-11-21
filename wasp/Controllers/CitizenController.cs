using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Models;
using WASP.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WASP.Utilities;
using WASP.Models.DTOs;
using WASP.Objects;

namespace WASP.Controllers
{
    [ApiController]
    [Route("WASP/Citizen/[action]")]
    public class CitizenController : BaseController
    {
        public CitizenController(IDbContextFactory<HiveContext> contextFactory) : base(contextFactory)
        {

        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse<CitizenDTO>>> SignUpCitizen(CitizenSignUpInputDTO citizen)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.CitizenSignUp(citizen),
                (dataResponse) => new WASPResponse<CitizenDTO>(dataResponse.Result));
        }

        [HttpPost]
        public async Task<ActionResult<WASPResponse<CitizenDTO>>> LogInCitizen(CitizenLoginDTO citizen)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.CitizenLogIn(citizen),
                (dataResponse) => new WASPResponse<CitizenDTO>(dataResponse.Result));
        }

        [HttpGet]
        public async Task<ActionResult<WASPResponse<CitizenDTO>>> GetCitizen(int citizenId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.GetCitizen(citizenId),
                (dataResponse) => new WASPResponse<CitizenDTO>(dataResponse.Result));

        }

        [HttpPut]
        public async Task<ActionResult<WASPResponse>> UpdateCitizen(int citizenId, IEnumerable<WASPUpdate> updates)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.UpdateCitizen(citizenId, updates),
                (dataResponse) => new WASPResponse());
        }

        [HttpPut]
        public async Task<ActionResult<WASPResponse>> BlockCitizen(int citizenId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.BlockCitizen(citizenId),
                (dataResponse) => new WASPResponse());
        }

        [HttpPut]
        public async Task<ActionResult<WASPResponse>> UnblockCitizen(int citizenId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.UnblockCitizen(citizenId),
                (dataResponse) => new WASPResponse());
        }

        [HttpDelete]
        public async Task<ActionResult<WASPResponse>> DeleteCitizen(int citizenId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.DeleteCitizen(citizenId),
                (dataResponse) => new WASPResponse());
        }
        [HttpGet]
        public async Task<WASPResponse<bool>> IsBlockedCitizen(int citizenId)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.IsBlockedCitizen(citizenId),
                (dataResponse) => new WASPResponse<bool>(dataResponse.Result));
        }

    }
}
