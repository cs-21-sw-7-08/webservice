﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        private bool ResetDatabase { get; set; } = true;

        public MockHiveContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HiveContext>();
            optionsBuilder.UseInMemoryDatabase("Hive");
            // Get reset flag
            var resetDatabase = ResetDatabase;            
            ResetDatabase = false;
            // Return context
            return new MockHiveContext(optionsBuilder.Options, resetDatabase);
        }
    }
}
