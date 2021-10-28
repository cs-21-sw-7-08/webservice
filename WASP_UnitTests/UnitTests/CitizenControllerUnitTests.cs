using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Controllers;
using WASP.Models;
using WASP.Test.Model;

namespace WASP.Test.UnitTests
{
    [TestClass]
    public class CitizenControllerUnitTests
    {

        [TestMethod]
        public async Task BlockAUser()
        {
            //Arrange
            int testID = 3;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Assert
            var result = await controller.BlockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {
                var citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == testID);

                //Act
                Assert.IsTrue(citizen.IsBlocked);
                Assert.IsTrue(result.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task UnblockAUser()
        {
            //Arrange
            int testID = 4;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Assert
            var result = await controller.UnblockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {
                var citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == testID);

                //Act
                Assert.IsFalse(citizen.IsBlocked);
                Assert.IsTrue(result.IsSuccessful);
            }

        }
    }
}
