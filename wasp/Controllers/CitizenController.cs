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
    [Route("WASP/Citizen/[action]")]
    public class CitizenController : ControllerBase
    {
        DataService dataService = new();

        [HttpPost]
        public async Task<WASPResponse> SignUp(Citizen citizen)
        {
            await dataService.CitizenSignUp(citizen);
            return new WASPResponse();

        }

        [HttpGet]
        public async Task<WASPResponse> LogIn(Citizen citizen)
        {
            await dataService.CitizenLogIn(citizen);
            return new WASPResponse();

        }

        [HttpPut]
        public async Task<WASPResponse> BlockUser(int citizen_id)
        {
            await dataService.BlockCitizen(citizen_id);
            return new WASPResponse();

        }

        [HttpPut]
        public async Task<WASPResponse> UnblockUser(int citizen_id)
        {
            await dataService.UnblockCitizen(citizen_id);
            return new WASPResponse();

        }

        [HttpPut]
        public async Task<WASPResponse> DeleteUser(int citizen_id)
        {
            await dataService.DeleteCitizen(citizen_id);
            return new WASPResponse();

        }

    }
}
