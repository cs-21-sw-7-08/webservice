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
        public async Task<WASPResponse<CitizenDTO>> SignUpCitizen(CitizenSignUpDTO citizen)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.CitizenSignUp(citizen),
                (dataResponse) => new WASPResponse<CitizenDTO>(dataResponse.Result)
            );
        }

        [HttpPost]
        public async Task<WASPResponse<CitizenDTO>> LogInCitizen(CitizenLoginDTO citizen)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.CitizenLogIn(citizen),
                (dataResponse) => new WASPResponse<CitizenDTO>(dataResponse.Result));

        }

        [HttpPut]
        public async Task<WASPResponse> BlockCitizen(int citizen_id)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.BlockCitizen(citizen_id),
                (dataResponse) => new WASPResponse());

        }

        [HttpPut]
        public async Task<WASPResponse> UnblockCitizen(int citizen_id)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.UnblockCitizen(citizen_id),
                (dataResponse) => new WASPResponse());

        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteCitizen(int citizen_id)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.DeleteCitizen(citizen_id),
                (dataResponse) => new WASPResponse());

        }
        [HttpGet]
        public async Task<WASPResponse<bool>> IsBlockedCitizen(int citizen_id)
        {
            return await ControllerUtil.GetResponse(
                async () => await DataService.IsBlockedCitizen(citizen_id),
                (dataResponse) => new WASPResponse<bool>(dataResponse.Result));

        }

    }
}
