using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WASP.Interfaces;
using WASP.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WASP.Objects;
using WASP.Enums;

namespace WASP.DataAccessLayer
{
    public partial class DataService : IDataService
    {
        #region Properties

        public IDbContextFactory<HiveContext> ContextFactory { get; set; }

        #endregion

        #region Constructor

        public DataService(IDbContextFactory<HiveContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        #endregion        
    }
}
