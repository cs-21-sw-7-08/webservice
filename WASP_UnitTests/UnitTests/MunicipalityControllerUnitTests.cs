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
        public async Task MunicipalityController_CreateResponse_InsertResponse_Succesful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityResponseInputDTO testResponse = new()
            {
                Id = 50,
                IssueId = 1,
                MunicipalityUserId = 1,
                Response = "test response"
            };
            MunicipalityController controller = new(contextFactory);

            //Act
            var result = await controller.CreateResponse(testResponse);
            
            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.MunicipalityResponses.FirstOrDefault(x => x.Id == result.Result.Id);
                //Assert
                Assert.AreEqual(testResponse.Id, response.Id);
            }

        }
        [TestMethod]
        public async Task MunicipalityController_DeleteResponse_DeleteResponse_Succesful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 1;

            //Act
            var result = await controller.DeleteResponse(responseId);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.MunicipalityResponses.FirstOrDefault(x => x.Id == responseId);
                //Assert
                Assert.IsNull(response);
            }
        }
        [TestMethod]
        public async Task MunicipalityController_DeleteResponse_ResponseDoesNotExist_ResponseError304()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityController controller = new(contextFactory);
            int responseId = 50;
            int errorNo = (int)ResponseErrors.ResponseDoesNotExist;

            //Act
            var result = await controller.DeleteResponse(responseId);

            //Assert
            Assert.AreEqual(errorNo, result.ErrorNo);
        }
        [TestMethod]
        public async Task MunicipalityController_UpdateResponse_UpdateResponseString_Succesful()
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
            var result = await controller.UpdateResponse(responseId, updates);

            using (var context = contextFactory.CreateDbContext())
            {
                var response = context.MunicipalityResponses.FirstOrDefault(x => x.Id == responseId);
                //Assert
                Assert.AreEqual("This is a test", response.Response);
            }
        }
        [TestMethod]
        public async Task MunicipalityController_UpdateResponse_WASPUpdateListBadFormat_ResponseError50()
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
            var result = await controller.UpdateResponse(responseId, updates);

            //Assert
            Assert.AreEqual(errorNo, result.ErrorNo);
        }
        [TestMethod]
        public async Task MunicipalityController_UpdateResponse_ResponseDoesNotExist_ResponseError304()
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
            var result = await controller.UpdateResponse(responseId, updates);

            //Assert
            Assert.AreEqual(errorNo, result.ErrorNo);
        }
        [TestMethod]
        public async Task MunicipalityController_UpdateResponse_ExceptionHandlingInGetResponse_ResponseError2()
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
            var result = await controller.UpdateResponse(responseId, updates);

            //Assert
            Assert.AreEqual(errorNo, result.ErrorNo);
        }
        [TestMethod]
        public async Task MunicipalityController_MunicipalitySignUp_InsertUser_Succesful()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityUserSignUpInputDTO testUser = new()
            {
                Id = 50,
                Email = "test@test.com",
                Name = "test",
                Password = "12345678"
            };
            MunicipalityController controller = new(contextFactory);

            //Act
            var result = await controller.SignUp(testUser);

            using (var context = contextFactory.CreateDbContext())
            {
                var user = context.MunicipalityUsers.FirstOrDefault(x => x.Id == result.Result.Id);
                //Assert
                Assert.AreEqual(testUser.Id, user.Id);
            }
        }
        [TestMethod]
        public async Task MunicipalityController_MunicipalitySignUp_UsingAnAlreadyUsedEmail_ErrorNo305()
        {
            //Arrange
            var contextFactory = new MockHiveContextFactory();
            MunicipalityUserSignUpInputDTO testUser = new()
            {
                Id = 50,
                Email = "grete@aalborg.dk",
                Name = "test",
                Password = "12345678"
            };
            MunicipalityController controller = new(contextFactory);
            int errorNo = (int)ResponseErrors.MunicipalityUserSignUpEmailIsAlreadyUsed;

            //Act
            var result = await controller.SignUp(testUser);

            //Assert
            Assert.AreEqual(errorNo, result.ErrorNo);
            
        }
    }
}
