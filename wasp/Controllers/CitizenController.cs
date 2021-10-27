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
    [Route("WASP/Citizen/[action]")]
    public class CitizenController : BaseController
    {
        public CitizenController(IDbContextFactory<HiveContext> contextFactory) : base(contextFactory)
        {

        }

        [HttpPost]
        public async Task<WASPResponse> SignUp(Citizen citizen)
        {
            await DataService.CitizenSignUp(citizen);
            return new WASPResponse();

        }

        [HttpGet]
        public async Task<WASPResponse> LogIn(Citizen citizen)
        {
            await DataService.CitizenLogIn(citizen);
            return new WASPResponse();

        }

        [HttpPut]
        public async Task<WASPResponse> BlockUser(int citizen_id)
        {
            await DataService.BlockCitizen(citizen_id);
            return new WASPResponse();

        }

        [HttpPut]
        public async Task<WASPResponse> UnblockUser(int citizen_id)
        {
            await DataService.UnblockCitizen(citizen_id);
            return new WASPResponse();

        }

        [HttpDelete]
        public async Task<WASPResponse> DeleteUser(int citizen_id)
        {
            await DataService.DeleteCitizen(citizen_id);
            return new WASPResponse();

        }

    }
}
