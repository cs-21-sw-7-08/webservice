using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Controllers;
using WASP.Models;
using WASP.Models.DTOs;
using WASP.Test.Model;
using WASP.Enums;

namespace WASP.Test.UnitTests
{
    [TestClass]
    public class CitizenControllerUnitTests
    {

        [TestMethod]
        [TestCategory(nameof(CitizenController.BlockCitizen))]
        public async Task CitizenController_BlockCitizen_Successful()
        {
            //Arrange
            int testId = 3;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            //Block a user that is not blocked
            var result = await controller.BlockCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                // Return citizen with given Id
                var citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == testId);

                //Assert

                //Verify that the citizen has become blocked & the result was sucessful
                Assert.IsTrue(citizen.IsBlocked);
                Assert.IsTrue(result.Value.IsSuccessful);
            }

        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.UnblockCitizen))]
        public async Task CitizenController_UnblockCitizen_Successful()
        {
            //Arrange
            int testId = 5;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            //Unblock a user that has been blocked
            var result = await controller.UnblockCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                var citizen = await context.Citizens.FirstOrDefaultAsync(cit => cit.Id == testId);

                //Assert

                //Check if the user has been blocked & the result was successful.
                Assert.IsFalse(citizen.IsBlocked);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.BlockCitizen))]
        public async Task CitizenController_BlockCitizen_CitizenAlreadyBlocked_ErrorNo203()
        {
            //Arrange
            int testId = 5;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            //Attempt to block a user that is already blocked
            var result = await controller.BlockCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert

                //Verify that the function return the relevant error code
                Assert.AreEqual((int)ResponseErrors.CitizenAlreadyBlocked, (int)result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.UnblockCitizen))]
        public async Task CitizenController_UnblockUser_CitizenAlreadyUnblocked_ErrorNo204()
        {
            //Arrange
            int testId = 2;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            //Attempt to unblock a user that is already unblocked
            var result = await controller.UnblockCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert

                //Verify that the function return the relevant error code
                Assert.AreEqual((int)ResponseErrors.CitizenAlreadyUnblocked, (int)result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.BlockCitizen))]
        public async Task CitizenController_BlockUser_CitizenDoesNotExist_ErrorNo200()
        {
            //Arrange
            int testId = 99;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act
            // Attempt block & unblock functions on non-existant Id
            var blockResult = await controller.BlockCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                //Verify that the functions return the relevant error codes
                Assert.AreEqual((int)ResponseErrors.CitizenDoesNotExist, (int)blockResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.UnblockCitizen))]
        public async Task CitizenController_UnblockUser_CitizenDoesNotExist_ErrorNo200()
        {
            //Arrange
            int testId = 99;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act
            // Attempt block & unblock functions on non-existant Id
            var unblockResult = await controller.UnblockCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert
                //Verify that the functions return the relevant error codes
                Assert.AreEqual((int)ResponseErrors.CitizenDoesNotExist, (int)unblockResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpEmail_InsertCitizen_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpDTO testCitizen = new()
            {
                Id = 50,
                Email = "test@test.com",
                Name = "test"
            };
            CitizenController controller = new(contextFactory);

            //Act
            var result = await controller.SignUpCitizen(testCitizen);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.Citizens.FirstOrDefault(x => x.Id == result.Value.Result.Id);
                //Assert
                Assert.AreEqual(testCitizen.Id, response.Id);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpPhoneNo_InsertCitizen_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpDTO testCitizen = new()
            {
                Id = 50,
                PhoneNo = "12345679",
                Name = "test"
            };
            CitizenController controller = new(contextFactory);
            //Act
            var result = await controller.SignUpCitizen(testCitizen);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.Citizens.FirstOrDefault(x => x.Id == result.Value.Result.Id);
                //Assert
                Assert.AreEqual(testCitizen.Id, response.Id);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpPhoneNo_CitizenSignUpPhoneNoIsAlreadyUsed_ErrorNo205()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpDTO testCitizen = new()
            {
                Id = 50,
                PhoneNo = "12345678",
                Name = "test"
            };
            CitizenController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.CitizenSignUpPhoneNoIsAlreadyUsed;
            //Act
            var result = await controller.SignUpCitizen(testCitizen);
            //Assert
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpEmail_CitizenSignUpEmailIsAlreadyUsed_ErrorNo206()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpDTO testCitizen = new()
            {
                Id = 50,
                Email = "email@email.dk",
                Name = "test"
            };
            CitizenController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.CitizenSignUpEmailIsAlreadyUsed;
            //Act
            var result = await controller.SignUpCitizen(testCitizen);
            //Assert
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpEmail_CitizenSignUpInvalidParametersEmailAndPhoneNoFilledOut_ErrorNo207()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpDTO testCitizen = new()
            {
                Id = 50,
                Email = "email@email.dk",
                PhoneNo = "12345679",
                Name = "test"
            };
            CitizenController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.CitizenSignUpInvalidParameters;
            //Act
            var result = await controller.SignUpCitizen(testCitizen);
            //Assert
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpEmail_CitizenSignUpInvalidParametersEmailAndPhoneNoNull_ErrorNo207()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpDTO testCitizen = new()
            {
                Id = 50,
                Name = "test"
            };
            CitizenController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.CitizenSignUpInvalidParameters;
            //Act
            var result = await controller.SignUpCitizen(testCitizen);
            //Assert
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.LogInCitizen))]
        public async Task CitizenController_LogIn_LogInWithEmail_Successful()
        {
            //Arrange
            CitizenLoginDTO testId = new CitizenLoginDTO();
            testId.Email = "email@email.dk";

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act
            var result = await controller.LogInCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.LogInCitizen))]
        public async Task CitizenController_LogIn_LogInWithEmail_ErrorNo202()
        {
            //Arrange
            CitizenLoginDTO testId = new CitizenLoginDTO();
            testId.Email = "email@emailyolo.dk";

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act
            var result = await controller.LogInCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(result.Value.ErrorNo, (int)ResponseErrors.CitizenWithTheseCredentialsHasNotBeenSignedUp);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.LogInCitizen))]
        public async Task CitizenController_LogIn_LogInWithPhoneNo_Successful()
        {
            //Arrange
            CitizenLoginDTO testId = new CitizenLoginDTO();
            testId.PhoneNo = "12345678";

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var result = await controller.LogInCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.IsTrue(result.Value.IsSuccessful);
            }

        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.LogInCitizen))]
        public async Task CitizenController_LogIn_LogInWithPhoneNo_ErrorNo202()
        {
            //Arrange
            CitizenLoginDTO testId = new CitizenLoginDTO();
            testId.PhoneNo = "12345679";

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var result = await controller.LogInCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(result.Value.ErrorNo, (int)ResponseErrors.CitizenWithTheseCredentialsHasNotBeenSignedUp);
            }

        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.LogInCitizen))]
        public async Task CitizenController_LogIn_LogInWithBothFilled_ErrorNo201()
        {
            //Arrange
            CitizenLoginDTO testId = new CitizenLoginDTO();
            testId.PhoneNo = "12345678";
            testId.Email = "email@email.dk";

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var result = await controller.LogInCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(result.Value.ErrorNo, (int)ResponseErrors.CitizenLoginBothEmailAndPhoneNumberCannotBeFilled);
            }

        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.LogInCitizen))]
        public async Task CitizenController_LogIn_LogInWithNullValues_ErrorNo201()
        {
            //Arrange
            CitizenLoginDTO testId = new CitizenLoginDTO();

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var result = await controller.LogInCitizen(testId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(result.Value.ErrorNo, (int)ResponseErrors.CitizenLoginBothEmailAndPhoneNumberCannotBeFilled);
            }

        }
        [TestMethod]
        [TestCategory(nameof(CitizenController.IsBlockedCitizen))]
        public async Task CitizenController_IsBlockedCitizen_true()
        {
            //Arrange
            int citizenId = 5;

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var response = await controller.IsBlockedCitizen(citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual((await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId)).IsBlocked, response.Result);
                Assert.IsTrue(response.IsSuccessful);
                Assert.AreEqual(response.Result, true);
            }

        }
        [TestMethod]
        [TestCategory(nameof(CitizenController.IsBlockedCitizen))]
        public async Task CitizenController_IsBlockedCitizen_false()
        {
            //Arrange
            int citizenId = 3;

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var response = await controller.IsBlockedCitizen(citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual((await context.Citizens.FirstOrDefaultAsync(x => x.Id == citizenId)).IsBlocked, response.Result);
                Assert.IsTrue(response.IsSuccessful);
                Assert.AreEqual(response.Result, false);
            }

        }
        [TestMethod]
        [TestCategory(nameof(CitizenController.IsBlockedCitizen))]
        public async Task CitizenController_IsBlockedCitizen_CitizenDoesNotExist_ErrorNo200()
        {
            //Arrange
            //This Id should not exist
            int citizenId = 999999;

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);
            int ErrorNo = (int)ResponseErrors.CitizenDoesNotExist;

            //Act
            var response = await controller.IsBlockedCitizen(citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(ErrorNo, response.ErrorNo);
            }

        }
    }
}