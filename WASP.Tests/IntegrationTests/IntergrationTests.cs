using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WASP.Enums;
using WASP.Models.DTOs;

namespace WASP.Test.IntegrationTests
{
    [TestClass]
    public class IntergrationTests
    {
        /// <summary>
        /// Sets up a local test server where the configuration is giving through the appsettings file.
        /// </summary>
        /// <returns>a test server</returns>
        private TestServer GetTestServer()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var webHostBuilder =
                  new WebHostBuilder()
                  .UseConfiguration(configurationBuilder)
                        .UseEnvironment("production") // You can set the environment you want (development, staging, production)
                        .UseStartup<Startup>(); // Startup class of your web app project
            // Return test server
            return new TestServer(webHostBuilder);
        }

        [TestMethod]
        public async Task IssueController_GetIssueDetails_CorrectJSONResponse()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {   
                // Act
                HttpResponseMessage responseMessage = await client.GetAsync("/WASP/Issue/GetIssueDetails?issueId=1");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"Result\":{\"Id\":1,\"CitizenId\":1,\"Description\":\"Der er en løs flise\",\"DateCreated\":\"2021-10-21T13:44:15\",\"Location\":{\"Latitude\":57.012218,\"Longitude\":9.99433},\"Address\":\"Alfred Nobels Vej 27, 9200 Aalborg, Danmark\",\"Category\":{\"Id\":3,\"Name\":\"Fortov\"},\"SubCategory\":{\"Id\":11,\"CategoryId\":3,\"Name\":\"Løse fliser\"},\"Municipality\":{\"Id\":1,\"Name\":\"Aalborg\"},\"IssueState\":{\"Id\":1,\"Name\":\"Oprettet\"},\"MunicipalityResponses\":[],\"IssueVerificationCitizenIds\":[2,7]},\"IsSuccessful\":true,\"ErrorNo\":0}",
                                 content);
            }
        }

        [TestMethod]
        public async Task IssueController_GetIssueDetails_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                HttpResponseMessage responseMessage = await client.GetAsync("/WASP/Issue/GetIssueDetails?issueId=99");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"IsSuccessful\":false,\"ErrorNo\":104}", content);
            }
        }

        [TestMethod]
        public async Task CitizenController_SignUpCitizen_CorrectJSONResponse()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                var citizen = new CitizenSignUpInputDTO
                {
                    Email = "test@test.com",
                    PhoneNo = null,
                    Name = "test",
                    MunicipalityId = 1
                };

                JsonContent jsonContent = JsonContent.Create(citizen);
                HttpResponseMessage responseMessage = await client.PostAsync("/WASP/Citizen/SignUpCitizen", jsonContent);
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"Result\":{\"Id\":12,\"Email\":\"test@test.com\",\"Name\":\"test\",\"IsBlocked\":false,\"Municipality\":{\"Id\":1,\"Name\":\"Aalborg\"}},\"IsSuccessful\":true,\"ErrorNo\":0}",
                                content);
            }
        }

        [TestMethod]
        public async Task CitizenController_SignUpCitizen_EmailIsAlreadyUsed_ErrorNo206()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                var citizen = new CitizenSignUpInputDTO
                {
                    Email = "email@email.dk",
                    PhoneNo = null,
                    Name = "test",
                    MunicipalityId = 2
                };

                JsonContent jsonContent = JsonContent.Create(citizen);
                HttpResponseMessage responseMessage = await client.PostAsync("/WASP/Citizen/SignUpCitizen", jsonContent);
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"IsSuccessful\":false,\"ErrorNo\":206}", content);
            }
        }

        [TestMethod]
        public async Task MunicipalityController_DeleteMunicipalityResponse_CorrectJSONResponse()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                HttpResponseMessage responseMessage = await client.DeleteAsync("/WASP/Municipality/DeleteMunicipalityResponse?responseId=1");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"IsSuccessful\":true,\"ErrorNo\":0}", content);
            }
        }

        [TestMethod]
        public async Task MunicipalityController_DeleteMunicipalityResponse_ResponseDoesNotExist_ErrorNo304()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                HttpResponseMessage responseMessage = await client.DeleteAsync("/WASP/Municipality/DeleteMunicipalityResponse?responseId=99");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"IsSuccessful\":false,\"ErrorNo\":304}", content);
            }
        }

        [TestMethod]
        public async Task WrongEndpoint_WrongControllerName_NotFound404()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                HttpResponseMessage responseMessage = await client.GetAsync("/WASP/Is/GetIssueDetails?issueId=1");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, responseMessage.StatusCode);
            }
        }

        [TestMethod]
        public async Task WrongEndpoint_WrongActionName_NotFound404()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                HttpResponseMessage responseMessage = await client.GetAsync("/WASP/Issue/GetIssueTails?issueId=1");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, responseMessage.StatusCode);
            }
        }
    }
}
