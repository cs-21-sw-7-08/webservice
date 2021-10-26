using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Models;

namespace WASP.Test.Model
{
    public class HiveContextFactory : IDbContextFactory<HiveContext>
    {
        public HiveContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HiveContext>();
            optionsBuilder.UseInMemoryDatabase("Hive");
            return new HiveContext(optionsBuilder.Options);
        }
    }
}
