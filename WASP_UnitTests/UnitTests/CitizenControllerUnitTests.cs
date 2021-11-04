﻿using Microsoft.EntityFrameworkCore;
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
        public async Task CitizenError_UserAlreadyBlocked()
        {
            //Arrange
            int testID = 4;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            //Attempt to block a user that is already blocked
            var result = await controller.BlockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert

                //Verify that the function return the relevant error code
                Assert.AreEqual((int)ResponseErrors.CitizenAlreadyBlocked, (int)result.ErrorNo);
            }
        }

        [TestMethod]
        public async Task CitizenError_UserAlreadyUnblocked()
        {
            //Arrange
            int testID = 2;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            //Attempt to unblock a user that is already unblocked
            var result = await controller.UnblockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert

                //Verify that the function return the relevant error code
                Assert.AreEqual((int)ResponseErrors.CitizenAlreadyUnblocked, (int)result.ErrorNo);
            }
        }

        [TestMethod]
        public async Task CitizenError_UserDoesNotExist()
        {
            //Arrange
            int testID = 99;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            //Act

            // Attempt block & unblock functions on non-existant ID
            var blockResult = await controller.BlockUser(testID);
            var unblockResult = await controller.UnblockUser(testID);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert

                //Verify that the functions return the relevant error codes
                Assert.AreEqual((int)ResponseErrors.CitizenDoesNotExist, (int)blockResult.ErrorNo);
                Assert.AreEqual((int)ResponseErrors.CitizenDoesNotExist, (int)unblockResult.ErrorNo);
            }
        }
        [TestMethod]
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
            var result = await controller.SignUp(testCitizen);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.Citizens.FirstOrDefault(x => x.Id == result.Result.Id);
                //Assert
                Assert.AreEqual(testCitizen.Id, response.Id);
            }
        }
        [TestMethod]
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
            var result = await controller.SignUp(testCitizen);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.Citizens.FirstOrDefault(x => x.Id == result.Result.Id);
                //Assert
                Assert.AreEqual(testCitizen.Id, response.Id);
            }
        }
        [TestMethod]
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
            var result = await controller.SignUp(testCitizen);

            //Assert
            Assert.AreEqual(result.ErrorNo, errorNo);
        }
        [TestMethod]
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
            var result = await controller.SignUp(testCitizen);

            //Assert
            Assert.AreEqual(result.ErrorNo, errorNo);
        }
        [TestMethod]
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
            var result = await controller.SignUp(testCitizen);

            //Assert
            Assert.AreEqual(result.ErrorNo, errorNo);
        }
        [TestMethod]
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
            var result = await controller.SignUp(testCitizen);

            //Assert
            Assert.AreEqual(result.ErrorNo, errorNo);
        }
    }
}