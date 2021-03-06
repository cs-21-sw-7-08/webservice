using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WASP.Controllers;
using WASP.Enums;
using WASP.Models;
using WASP.Models.DTOs;
using WASP.Objects;
using WASP.Test.Model;
using WASP.Utilities;

namespace WASP.Test.UnitTests
{
    [TestClass]
    public class MunicipalityControllerUnitTests
    {
        [TestMethod]
        [TestCategory(nameof(MunicipalityController.CreateMunicipalityResponse))]
        public async Task MunicipalityController_CreateResponse_InsertResponse_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityResponseInputDTO testResponse = new()
            {
                IssueId = 1,
                MunicipalityUserId = 1,
                Response = "test response"
            };
            MunicipalityController controller = new(contextFactory);

            //Act
            var result = await controller.CreateMunicipalityResponse(testResponse);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.MunicipalityResponses.FirstOrDefault(x => x.Response == testResponse.Response && x.IssueId == testResponse.IssueId);
                //Assert
                Assert.AreEqual(testResponse.IssueId, response.IssueId);
                Assert.AreEqual(testResponse.MunicipalityUserId, response.MunicipalityUserId);
                Assert.AreEqual(testResponse.Response, response.Response);
            }
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.CreateMunicipalityResponse))]
        public async Task MunicipalityController_CreateResponse_IssuesDoesNotExist_ErrorNo104()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityResponseInputDTO testResponse = new()
            {
                IssueId = 50,
                MunicipalityUserId = 1,
                Response = "test response"
            };
            MunicipalityController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.IssueDoesNotExist;

            //Act
            var result = await controller.CreateMunicipalityResponse(testResponse);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.CreateMunicipalityResponse))]
        public async Task MunicipalityController_CreateResponse_MunicipalityUserMunicipalityIdDoesNotMatchIssueId_ErrorNo303()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityResponseInputDTO testResponse = new()
            {
                IssueId = 2,
                MunicipalityUserId = 1,
                Response = "test response"
            };
            MunicipalityController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.MunicipalityUserMunicipalityIdDoesNotMatchIssueMunicipalityId;

            //Act
            var result = await controller.CreateMunicipalityResponse(testResponse);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.DeleteMunicipalityResponse))]
        public async Task MunicipalityController_DeleteResponse_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 1;

            //Act
            var result = await controller.DeleteMunicipalityResponse(responseId);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.MunicipalityResponses.FirstOrDefault(x => x.Id == responseId);
                //Assert
                Assert.IsNull(response);
            }
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.DeleteMunicipalityResponse))]
        public async Task MunicipalityController_DeleteResponse_ResponseDoesNotExist_ErrorNo304()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 50;
            int errorNo = (int)ResponseErrors.ResponseDoesNotExist;

            //Act
            var result = await controller.DeleteMunicipalityResponse(responseId);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.UpdateMunicipalityResponse))]
        public async Task MunicipalityController_UpdateResponse_UpdateResponseString_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 1;
            List<WASPUpdate> updates = new List<WASPUpdate>()
            {
                new()
                {
                    Name = nameof(MunicipalityResponse.Response),
                    Value = "This is a test"
                }
            };

            //Act
            var result = await controller.UpdateMunicipalityResponse(responseId, updates);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.MunicipalityResponses.FirstOrDefault(x => x.Id == responseId);
                //Assert
                Assert.AreEqual("This is a test", response.Response);
            }
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.UpdateMunicipalityResponse))]
        public async Task MunicipalityController_UpdateResponse_WASPUpdateListBadFormat_ErrorNo50()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 1;
            List<WASPUpdate> updates = new List<WASPUpdate>()
            {
                new()
                {

                }
            };
            int errorNo = (int)ResponseErrors.WASPUpdateListBadFormat;

            //Act
            var result = await controller.UpdateMunicipalityResponse(responseId, updates);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.UpdateMunicipalityResponse))]
        public async Task MunicipalityController_UpdateResponse_ResponseDoesNotExist_ErrorNo304()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 50;
            List<WASPUpdate> updates = new List<WASPUpdate>()
            {
                new()
                {
                    Name = nameof(MunicipalityResponse.Response),
                    Value = "This is a test"
                }
            };
            int errorNo = (int)ResponseErrors.ResponseDoesNotExist;

            //Act
            var result = await controller.UpdateMunicipalityResponse(responseId, updates);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.UpdateMunicipalityResponse))]
        public async Task MunicipalityController_UpdateResponse_ExceptionHandlingInGetResponse_ErrorNo2()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 1;
            List<WASPUpdate> updates = new List<WASPUpdate>()
            {
                new()
                {
                    Name = nameof(MunicipalityResponse.Response),
                    Value = JsonSerializer.Deserialize("{\"test\":\"This is a test\"}", typeof(JsonElement))
                }
            };
            int errorNo = (int)ResponseErrors.AnExceptionOccurredInTheDAL;

            //Act
            var result = await controller.UpdateMunicipalityResponse(responseId, updates);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }
        [TestMethod]
        [TestCategory(nameof(MunicipalityController.SignUpMunicipality))]
        public async Task MunicipalityController_SignUpMunicipality_InsertUser_Successful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityUserSignUpInputDTO testUser = new()
            {
                Email = "test@test.com",
                Name = "test",
                Password = "12345678",
                MunicipalityId = 1
            };
            MunicipalityController controller = new(contextFactory);

            //Act
            var result = await controller.SignUpMunicipality(testUser);

            using (var context = contextFactory.CreateDbContext())
            {
                var user = context.MunicipalityUsers.FirstOrDefault(x => x.Email == testUser.Email);
                //Assert
                Assert.AreEqual(testUser.Email, user.Email);
            }
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.SignUpMunicipality))]
        public async Task MunicipalityController_SignUpMunicipality_MunicipalityUserSignUpEmailIsAlreadyUsed_ErrorNo305()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityUserSignUpInputDTO testUser = new()
            {
                Email = "grete@aalborg.dk",
                Name = "test",
                Password = "12345678",
                MunicipalityId = 1
            };
            MunicipalityController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.MunicipalityUserSignUpEmailIsAlreadyUsed;

            //Act
            var result = await controller.SignUpMunicipality(testUser);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);

        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.SignUpMunicipality))]
        public async Task MunicipalityController_SignUpMunicipality_MunicipalityDoesNotExist_ErrorNo300()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityUserSignUpInputDTO testUser = new()
            {
                Email = "test@test.com",
                Name = "test",
                Password = "12345678",
                MunicipalityId = 50
            };
            MunicipalityController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.MunicipalityDoesNotExist;

            //Act
            var result = await controller.SignUpMunicipality(testUser);

            //Assert
            Assert.AreEqual(errorNo, result.Value.ErrorNo);

        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.LogInMunicipality))]
        public async Task MunicipalityController_LogInMunicipality_MunicipalityUserEmailAndOrPasswordNotMatched_Error302()
        {
            //Arrange
            MunicipalityUserLoginDTO testMuni = new MunicipalityUserLoginDTO();
            testMuni.Email = "gretethebadass@aalborg.dk";
            testMuni.Password = "12345678";
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);

            //Act
            var result = await controller.LogInMunicipality(testMuni);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.AreEqual(result.Value.ErrorNo, (int)ResponseErrors.MunicipalityUserEmailAndOrPasswordNotMatched);
            }

        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.LogInMunicipality))]
        public async Task MunicipalityController_LogInMunicipality_Successful()
        {
            //Arrange
            MunicipalityUserLoginDTO testMuni = new MunicipalityUserLoginDTO();
            testMuni.Email = "grete@aalborg.dk";
            testMuni.Password = "12345678";
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);


            //Act
            var result = await controller.LogInMunicipality(testMuni);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.IsTrue(result.Value.IsSuccessful);
            }

        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.LogInMunicipality))]
        public async Task MunicipalityController_LogInMunicipality_Successful_LoginDTOConstructor()
        {
            //Arrange
            MunicipalityUser testUser = new MunicipalityUser()
            {
                Name = "Annette",
                MunicipalityId = 1,
                Email = "annette@aalborg.dk",
                Password = "12345678",
            };
            MunicipalityUserLoginDTO muniDTO = new MunicipalityUserLoginDTO(testUser);
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);


            //Act
            var result = await controller.LogInMunicipality(muniDTO);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(MunicipalityController.GetListOfMunicipalities))]
        public async Task MunicipalityController_GetListOfMunicipalities_Successful()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            MunicipalityController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfMunicipalities();
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Municipalities.Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }
    }
}
