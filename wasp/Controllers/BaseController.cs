using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Interfaces;
using WASP.Models;
using WASP.DataAccessLayer;

namespace WASP.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IDbContextFactory<HiveContext> ContextFactory { get; set; }
        protected IDataService DataService { get; set; }

        public BaseController(IDbContextFactory<HiveContext> contextFactory)
        {
            ContextFactory = contextFactory;
            DataService = new DataService(ContextFactory);
        }

    }
}
