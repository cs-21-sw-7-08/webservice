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
    [Route("WASP/Municipality")]
    public class MunicipalityController : ControllerBase
    {
        DataService dataService = new(); 
    }
}
