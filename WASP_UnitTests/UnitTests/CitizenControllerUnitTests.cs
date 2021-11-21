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
using WASP.Objects;

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
            CitizenSignUpInputDTO testCitizen = new()
            {
                Email = "test@test.com",
                Name = "test",
                MunicipalityId = 2
            };
            CitizenController controller = new(contextFactory);

            //Act
            var result = await controller.SignUpCitizen(testCitizen);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.Citizens.FirstOrDefault(x => x.Id == result.Value.Result.Id);
                //Assert
                Assert.IsInstanceOfType(result.Value.Result, typeof(CitizenDTO));
                Assert.AreEqual(testCitizen.Name, response.Name);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpPhoneNo_InsertCitizen_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpInputDTO testCitizen = new()
            {
                PhoneNo = "12345679",
                Name = "test",
                MunicipalityId = 1
            };
            CitizenController controller = new(contextFactory);
            //Act
            var result = await controller.SignUpCitizen(testCitizen);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.Citizens.FirstOrDefault(x => x.Id == result.Value.Result.Id);
                //Assert
                Assert.IsInstanceOfType(result.Value.Result, typeof(CitizenDTO));
                Assert.AreEqual(testCitizen.Name, response.Name);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.SignUpCitizen))]
        public async Task CitizenController_CitizenSignUpPhoneNo_CitizenSignUpPhoneNoIsAlreadyUsed_ErrorNo205()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpInputDTO testCitizen = new()
            {
                PhoneNo = "12345678",
                Name = "test",
                MunicipalityId = 1
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
            CitizenSignUpInputDTO testCitizen = new()
            {
                Email = "email@email.dk",
                Name = "test",
                MunicipalityId = 1
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
            CitizenSignUpInputDTO testCitizen = new()
            {
                Email = "email@email.dk",
                PhoneNo = "12345679",
                Name = "test",
                MunicipalityId = 1
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
            CitizenSignUpInputDTO testCitizen = new()
            {
                Name = "test",
                MunicipalityId = 1
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
        public async Task CitizenController_CitizenSignUp_MunicipalityNotFound_ErrorNo300()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenSignUpInputDTO testCitizen = new()
            {
                PhoneNo = "11111111",
                Name = "test",
                MunicipalityId = 50
            };
            CitizenController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.MunicipalityDoesNotExist;
            //Act
            var result = await controller.SignUpCitizen(testCitizen);
            //Assert
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.GetCitizen))]
        public async Task CitizenController_GetCitizen_Succesful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            int citizenId = 3;
            
            //Act 
            // Attempt to update an issue property with a new one
            var result = await controller.GetCitizen(citizenId);

            // Obtain the issue after it's property is updated.
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert
                Citizen contextCit = await context.Citizens.FirstOrDefaultAsync(ncit => ncit.Id == citizenId);
                // Check if the description has been updated after using UpdateIssue
                Assert.AreEqual(contextCit.Name, result.Value.Result.Name);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.GetCitizen))]
        public async Task CitizenController_GetCitizen_ErrorCitizenNotFound_Error200()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);
            int ErrorNum = (int)ResponseErrors.CitizenDoesNotExist;
            int citizenId = -9;

            //Act 
            // Attempt to update an issue property with a new one
            var result = await controller.GetCitizen(citizenId);

            // Obtain the issue after it's property is updated.
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert
                Citizen contextCit = await context.Citizens.FirstOrDefaultAsync(ncit => ncit.Id == citizenId);
                // Check if the description has been updated after using UpdateIssue
                Assert.AreEqual(ErrorNum, result.Value.ErrorNo);
                Assert.IsFalse(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.UpdateCitizen))]
        public async Task CitizenController_UpdateCitizen_Succesful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            int citizenId = 4;
            IEnumerable<WASPUpdate> citizenUpdate = new List<WASPUpdate>()
            {
                new()
                {
                    Name = "Name",
                    Value = "Gert"
                },
                new()
                {
                    Name = "MunicipalityId",
                    Value = "1"
                }
            };

            //Act 
            // Attempt to update an issue property with a new one
            var result = await controller.UpdateCitizen(citizenId, citizenUpdate);

            // Obtain the issue after it's property is updated.
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert
                Citizen newCitizen = await context.Citizens.FirstOrDefaultAsync(ncit => ncit.Id == citizenId);
                // Check if the description has been updated after using UpdateIssue
                Assert.AreEqual(citizenUpdate.ElementAt(0).Value, newCitizen.Name);
                Assert.AreEqual(int.Parse(citizenUpdate.ElementAt(1).Value.ToString()), newCitizen.MunicipalityId);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.UpdateCitizen))]
        public async Task CitizenController_UpdateCitizen_CitizenDoesNotExist()
        {
            //Arrange
            int errorNum = (int)ResponseErrors.CitizenDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            int citizenId = 555;
            IEnumerable<WASPUpdate> citizenUpdate = new List<WASPUpdate>()
            {
                new()
                {
                    Name = "Name",
                    Value = "Pierre"
                },
                new()
                {
                    Name = "MunicipalityId",
                    Value = "2"
                }
            };

            //Act 
            // Attempt to update an issue property with a new one
            var result = await controller.UpdateCitizen(citizenId, citizenUpdate);

            // Obtain the issue after it's property is updated.
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert
                Assert.AreEqual(result.Value.ErrorNo, errorNum);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.UpdateCitizen))]
        public async Task CitizenController_UpdateCitizen_ErrorWASPUpdateListBadFormat()
        {
            //Arrange
            int errorNum = (int)ResponseErrors.WASPUpdateListBadFormat;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            int citizenId = 4;
            IEnumerable<WASPUpdate> citizenUpdate = new List<WASPUpdate>()
            {
                new()
                {
                    Name = "Address",
                    Value = "Humlevej 33"
                },
            };

            //Act 
            // Attempt to update an issue property with a new one
            var result = await controller.UpdateCitizen(citizenId, citizenUpdate);

            // Obtain the issue after it's property is updated.
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert
                Citizen newCitizen = await context.Citizens.FirstOrDefaultAsync(ncit => ncit.Id == citizenId);
                // Check if the description has been updated after using UpdateIssue
                Assert.AreEqual(result.Value.ErrorNo, errorNum);
            }
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

        [TestMethod]
        [TestCategory(nameof(CitizenController.DeleteCitizen))]
        public async Task CitizenController_DeleteCitizen_Successful()
        {
            //Arrange
            int citizenId = 4;

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);


            //Act
            var result = await controller.DeleteCitizen(citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.IsTrue(result.Value.IsSuccessful);
            }

        }

        [TestMethod]
        [TestCategory(nameof(CitizenController.DeleteCitizen))]
        public async Task CitizenController_DeleteCitizen_citizenIdInvalid_ErrorNo200()
        {
            //Arrange
            int citizenId = 99998;

            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);
            var ErrorNo = (int)ResponseErrors.CitizenDoesNotExist;


            //Act
            var result = await controller.DeleteCitizen(citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(result.Value.ErrorNo, ErrorNo);
            }

        }
    }
}