using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Models;

namespace WASP.Test.Model
{
    public class MockHiveContextFactory : IDbContextFactory<MockHiveContext>
    {
        public MockHiveContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HiveContext>();
            optionsBuilder.UseInMemoryDatabase("Hive");
            return new MockHiveContext(optionsBuilder.Options);
        }
    }
}
