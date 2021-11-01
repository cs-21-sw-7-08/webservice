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
using WASP.Enums;

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

            //Act

            //Block a user that is not blocked
            var result = await controller.BlockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {
                // Return citizen with given ID
                var citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == testID);

                //Assert

                //Verify that the citizen has become blocked & the result was sucessful
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

            //Act

            //Unblock a user that has been blocked
            var result = await controller.UnblockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {
                var citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == testID);

                //Assert

                //Check if the user has been blocked & the result was successful.
                Assert.IsFalse(citizen.IsBlocked);
                Assert.IsTrue(result.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task BlockErrorUserAlreadyBlocked()
        {
            //Arrange
            int testID = 4;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act
            var result = await controller.BlockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert
                Assert.AreEqual((int)ResponseErrors.CitizenAlreadyBlocked, (int)result.ErrorNo);
            }
        }

        [TestMethod]
        public async Task BlockErrorUserAlreadyUnblocked()
        {
            //Arrange
            int testID = 2;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act
            var result = await controller.UnblockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert
                Assert.AreEqual((int)ResponseErrors.CitizenAlreadyUnblocked, (int)result.ErrorNo);
            }
        }
    }
}