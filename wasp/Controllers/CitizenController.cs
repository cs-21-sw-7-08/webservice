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
    [Route("WASP/Citizen/[action]")]
    public class CitizenController : ControllerBase
    {
        DataService dataService = new();

        [HttpGet]

        public async Task<WASPResponse> CitizenSignUp(Citizen citizen)
        {
            await dataService.CitSignUp(citizen);
            return new WASPResponse();

        }

        public async Task<WASPResponse> CitizenLogIn(Citizen citizen)
        {
            await dataService.CitLogIn(citizen);
            return new WASPResponse();

        }

        public async Task<WASPResponse> BlockUser(int citizen_id)
        {
            await dataService.BlockUser(citizen_id);
            return new WASPResponse();

        }

        public async Task<WASPResponse> UnblockUser(int citizen_id)
        {
            await dataService.UnblockUser(citizen_id);
            return new WASPResponse();

        }

        public async Task<WASPResponse> DeleteUser(int citizen_id)
        {
            await dataService.DeleteUser(citizen_id);
            return new WASPResponse();

        }

    }
}
