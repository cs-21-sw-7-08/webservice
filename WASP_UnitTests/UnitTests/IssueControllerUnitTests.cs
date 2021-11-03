using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP;
using WASP.Models;
using WASP.DataAccessLayer;
using WASP.Controllers;
using System.Threading.Tasks;
using WASP.Test.Model;
using WASP.Objects;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using WASP.Enums;

namespace WASP.Test.UnitTests
{
    [TestClass]
    public class IssueControllerUnitTests
    {

        [TestMethod]
        public async Task GetDetailsOfIssue()
        {
            // Arrange
            int issueID = 2;
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var response = await controller.GetIssueDetails(issueID);
            using (var context = contextFactory.CreateDbContext())
            {
                //Get an issue with a given ID from the context
                var expectedIssue = context.Issues.FirstOrDefault(issue => issue.Id == issueID);

                //Obtain an issue with a given ID using the controller function
                var result = response.Value.Result;

                // Assert

                //Check if the result is not null
                Assert.IsNotNull(result);

                //Check if (some of) the issue info is identical for both the context- and controller result
                Assert.AreEqual(expectedIssue.Id, result.Id);
                Assert.AreEqual(expectedIssue.Description, result.Description);
                Assert.IsTrue(response.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task GetListOfIssues()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(new IssuesOverviewFilter());
            using (var context = contextFactory.CreateDbContext())
            {

                //Return the length of the issue-list in the context
                var expectedList = context.Issues.Count();

                // Assert

                //Verify that the length of the list is equal to the context issue-list length
                Assert.IsTrue(expectedList == result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task GetCategories()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfCategories();
            using (var context = contextFactory.CreateDbContext())
            {

                //Return the length of the issue-list in the context
                var expectedList = context.Categories.Count();

                // Assert

                //Verify that the length of the list is equal to the context issue-list length
                Assert.IsTrue(expectedList == result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task UpdateIssueInformation()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);
            int issueID = 1;
            IEnumerable<WASPUpdate> update = new List<WASPUpdate>()
                {
                    new()
                    {
                        Name = "Description",
                        Value = "Vejen er blevet beskidt"
                    }
                };

            // Act

            //Attempt to update an issue property with a new one
            var result = await controller.UpdateIssue(issueID, update);

            //Obtain the issue after it's property is updated.
            var newIssue = await controller.GetIssueDetails(issueID);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                // Check if the description has been updated after using UpdateIssue
                Assert.AreEqual(update.First().Value, newIssue.Value.Result.Description);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task CreateAnIssue()
        {
            //Arrange

            IssueCreateDTO mockIssueDTO = new()
            {
                CitizenId = 4,
                Description = "Der er graffiti på min væg",
                MunicipalityId = 1,
                SubCategoryId = 2,
                LocationPlaceHolder = new Location(57.012218, 9.994330)
            };
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Create the issue to the context
            var result = await controller.CreateIssue(mockIssueDTO);

            using (var context = contextFactory.CreateDbContext())
            {
                int issueID = context.Issues.Count();
                //Obtain the newly created issue
                var freshIssue = context.Issues.FirstOrDefault(issue => issue.Id == issueID);

                //Assert

                //Verify that the obtained issue is the same as the one created
                Assert.AreEqual(mockIssueDTO.Description, freshIssue.Description);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task DeletingIssue()
        {
            //Arrange
            int issueID = 3;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            // Ensure issue exists before it is deleted
            var retrivedDTO = await controller.GetIssueDetails(issueID);
            var retrivedIssue = retrivedDTO.Value.Result;

            //Attempt to return an issue using a non-existant ID
            var result = await controller.DeleteIssue(issueID);
            using (var context = contextFactory.CreateDbContext())
            {
                var retrieveAttempt = context.Issues.FirstOrDefault(issue => issue.Id == issueID);
                //Assert

                //Verify that the attempt to find issue does not find the issue
                Assert.IsNotNull(retrivedIssue);
                Assert.IsNull(retrieveAttempt);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task VerifyingIssue()
        {
            //Arrange
            int issueID = 1;
            int citizenID = 3;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            var beforeVerifs = await controller.GetIssueDetails(issueID);
            var result = await controller.VerifyIssue(issueID, citizenID);
            using (var context = contextFactory.CreateDbContext())
            {
                IssueVerification verif = context.IssueVerifications.FirstOrDefault(cit => cit.CitizenId == citizenID);

                //Assert

                //Verify that the issue-verification exists.
                Assert.IsNotNull(verif);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task ReportingIssue()
        {
            //Arrange
            int issueID = 2;
            int reportCategoryID = 1;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act
            var result = await controller.ReportIssue(issueID, reportCategoryID);
            using (var context = contextFactory.CreateDbContext())
            {
                Report report = context.Reports.FirstOrDefault(issue => issue.IssueId == issueID);

                //Assert

                //Verify that the report exists and it has one on its instances.
                Assert.IsNotNull(report);
                Assert.IsTrue(report.TypeCounter == 1);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task Report_IncreaseCounter()
        {
            //Arrange
            int issueID = 3;
            int reportCategoryID = 1;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Perform two reports of the same category on the same issue
            var result1 = await controller.ReportIssue(issueID, reportCategoryID);
            var result2 = await controller.ReportIssue(issueID, reportCategoryID);
            using (var context = contextFactory.CreateDbContext())
            {
                Report report = context.Reports.FirstOrDefault(issue => issue.IssueId == issueID);

                //Assert

                //Verify that a report could be found
                Assert.IsNotNull(report);

                //Verify that the issue now has two reports on it.
                Assert.IsTrue(report.TypeCounter == 2);
                Assert.IsTrue(result1.Value.IsSuccessful);
                Assert.IsTrue(result2.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task UpdateIssueStatus()
        {
            //Arrange
            int issueID1 = 1;
            int issueID2 = 2;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Update an issue's status to the next status (Created -> Approved & Approved -> Resolved) 
            var result1 = await controller.UpdateIssueStatus(issueID1, 2);
            var result2 = await controller.UpdateIssueStatus(issueID2, 3);
            using (var context = contextFactory.CreateDbContext())
            {
                //Get the state of the issue just updated.
                int stateIss1 = context.Issues.FirstOrDefault(issue => issue.Id == issueID1).IssueStateId;
                int stateIss2 = context.Issues.FirstOrDefault(issue => issue.Id == issueID2).IssueStateId;

                //Assert

                //Verify that the issue's new status is the expected new one.
                Assert.IsTrue(stateIss1 == 2);
                Assert.IsTrue(stateIss2 == 3);
                Assert.IsTrue(result1.Value.IsSuccessful);
                Assert.IsTrue(result2.Value.IsSuccessful);
            }
        }

        [TestMethod]
        public async Task IssueError_IssueDoesNotExist()
        {
            //Arrange
            int testID = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            IEnumerable<WASPUpdate> update = new List<WASPUpdate>()
                {
                    new()
                    {
                        Name = "Description",
                        Value = "Der er lort på fortorvet" // ;)
                    }
                };

            //Act

            //Attempt to return issues using a non-existant ID
            var getResult = await controller.GetIssueDetails(testID);
            var updateResult = await controller.UpdateIssue(testID, update);
            var statusResult = await controller.UpdateIssueStatus(testID, 3);
            var verifResult = await controller.VerifyIssue(testID, 3);
            var reportResult = await controller.ReportIssue(testID, 1);
            using (var context = contextFactory.CreateDbContext())
            {

                //Assert

                //Verify that the functions returns the relevant error code.
                Assert.AreEqual(notExistCode, getResult.Value.ErrorNo);
                Assert.AreEqual(notExistCode, updateResult.Value.ErrorNo);
                Assert.AreEqual(notExistCode, statusResult.Value.ErrorNo);
                Assert.AreEqual(notExistCode, verifResult.Value.ErrorNo);
                Assert.AreEqual(notExistCode, reportResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        public async Task IssueError_IssueStatusNotExist()
        {
            //Arrange
            int issueID = 1;
            int notExistCode = (int)ResponseErrors.IssueStateDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Attempt to update an issue's status to one that does not exist
            var result = await controller.UpdateIssueStatus(issueID, 69);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert

                //Verify the function returns the relevant error code
                Assert.AreEqual(notExistCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        public async Task IssueError_DisallowedStatusUpdate()
        {
            //Arrange
            int issueID1 = 2;
            int issueID2 = 3;
            int disallowedCode = (int)ResponseErrors.DisallowedIssueStateChange;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Attempt a status update that does not follow the normal convention (i.e going reverse)
            var result1 = await controller.UpdateIssueStatus(issueID1, 1);
            var result2 = await controller.UpdateIssueStatus(issueID2, 2);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert

                //Verify that the function returns the relevant error code
                Assert.AreEqual(disallowedCode, result1.Value.ErrorNo);
                Assert.AreEqual(disallowedCode, result2.Value.ErrorNo);
            }
        }

        [TestMethod]
        public async Task IssueError_VerifyByOwner()
        {
            //Arrange
            int issueID = 2;
            int citizenID = 2;
            int creatorErrorCode = (int)ResponseErrors.IssueCannotBeVerifiedByItsCreator;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Attempt to update an issue's status to one that does not exist
            var result = await controller.VerifyIssue(issueID, citizenID);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert

                //Verify the function returns the relevant error code
                Assert.AreEqual(creatorErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        public async Task IssueError_AlreadyVerified()
        {
            //Arrange
            int issueID = 3;
            int citizenID = 4;
            int creatorErrorCode = (int)ResponseErrors.IssueAlreadyVerifiedByThisCitizen;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            //Act

            //Verify an issue once
            var resultFirst = await controller.VerifyIssue(issueID, citizenID);

            //Attempt a verify again by the same civilian ID
            var resultSecond = await controller.VerifyIssue(issueID, citizenID);
            using (var context = contextFactory.CreateDbContext())
            {
                //Assert

                //Verify that verification works the first time
                Assert.IsTrue(resultFirst.Value.IsSuccessful);

                //Verify that attemping a second verification returns the relevant error
                Assert.AreEqual(creatorErrorCode, resultSecond.Value.ErrorNo);
            }
        }
    }
}
