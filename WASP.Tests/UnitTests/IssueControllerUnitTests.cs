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
using System;
using System.Text.Json;

namespace WASP.Test.UnitTests
{
    [TestClass]
    public class IssueControllerUnitTests
    {

        [TestMethod]
        [TestCategory(nameof(IssueController.GetIssueDetails))]
        public async Task IssueController_GetIssueDetails_Successful()
        {
            // Arrange
            int issueId = 2;
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var response = await controller.GetIssueDetails(issueId);
            using (var context = contextFactory.CreateDbContext())
            {
                // Get an issue with a given Id from the context
                var expectedIssue = context.Issues.FirstOrDefault(issue => issue.Id == issueId);

                // Obtain an issue with a given Id using the controller function
                var result = response.Value.Result;

                // Assert

                // Check if the result is not null
                Assert.IsNotNull(result);

                // Check if (some of) the issue info is identical for both the context- and controller result
                Assert.AreEqual(expectedIssue.Id, result.Id);
                Assert.AreEqual(expectedIssue.Description, result.Description);
                Assert.IsTrue(response.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_Successful()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(new IssuesOverviewFilter());
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues.Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterBlockedCIssues()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    IsBlocked = false
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => Issue.Citizen.IsBlocked == false)
                    .Count();

                // Assert
                // Check type of result
                Assert.IsInstanceOfType(result.Value.Result, typeof(IEnumerable<IssuesOverviewDTO>));
                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterUnblockedIssues()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    IsBlocked = true
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => Issue.Citizen.IsBlocked == true)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterIssueStates()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    IssueStateIds = new List<int>() { 2 },
                    CitizenIds = null
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => Issue.IssueStateId == 2)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }
        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterCategoryId()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    CategoryIds = new List<int>() { 2 }
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => Issue.CategoryId == 2)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterSubcategoryId()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    SubCategoryIds = new List<int>() { 3 }
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => Issue.SubCategoryId == 3)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterFromTime()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    FromTime = DateTime.Parse("2021-9-21 13:44:15")
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => DateTime.Compare(DateTime.Parse("2021-9-21 13:44:15"), Issue.DateCreated) <= 0)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterToTime()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    ToTime = DateTime.Parse("2021-12-21 13:44:15")
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => DateTime.Compare(DateTime.Parse("2021-12-21 13:44:15"), Issue.DateCreated) > 0)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfIssues))]
        public async Task IssueController_GetListOfIssues_FilterMunicipalityId()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfIssues(
                new IssuesOverviewFilter()
                {
                    MunicipalityIds = new List<int>() { 1 }
                });
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Issues
                    .Where(Issue => Issue.MunicipalityId == 1)
                    .Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfCategories))]
        public async Task IssueController_GetListOfCategories_Successful()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfCategories();
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.Categories.Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
                Assert.IsInstanceOfType(result.Value.Result, typeof(IEnumerable<CategoryListDTO>));
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssue))]
        public async Task IssueController_UpdateIssue_Successful()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);
            int issueId = 1;

            var jsonInput = @"{
                                ""Value"": ""Der er lort p? fortorvet""
                            }";
            var parsed = JsonDocument.Parse(jsonInput);
            var element = parsed.RootElement;
            var value = element.GetProperty("Value");
            
            IEnumerable<WASPUpdate> update = new List<WASPUpdate>()
                {
                    new()
                    {
                        Name = "Description",
                        Value = value,
                    },
                    new ()
                    {
                        Name = "SubCategoryId",
                        Value = 2
                    }
                };

            // Act

            // Attempt to update an issue property with a new one
            var result = await controller.UpdateIssue(issueId, update);

            // Obtain the issue after it's property is updated.
            var newIssue = await controller.GetIssueDetails(issueId);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                // Check if the description has been updated after using UpdateIssue
                Assert.AreEqual("Der er lort p? fortorvet", newIssue.Value.Result.Description);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.CreateIssue))]
        public async Task IssueController_CreateIssue_Successful()
        {
            // Arrange
            IssueCreateDTO mockIssueDTO = new()
            {
                CitizenId = 4,
                Description = "Der er graffiti p? min v?g",
                MunicipalityId = 1,
                SubCategoryId = 2,
                LocationPlaceHolder = new Location(57.012218, 9.994330),
                Address = "Alfred Nobels Vej 27, 9200 Aalborg, Danmark"
            };
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act            
            var result = await controller.CreateIssue(mockIssueDTO);

            using (var context = contextFactory.CreateDbContext())
            {
                int issueId = context.Issues.Count();
                // Obtain the newly created issue
                var freshIssue = context.Issues.FirstOrDefault(issue => issue.Id == issueId);

                // Assert                
                Assert.AreEqual(mockIssueDTO.Description, freshIssue.Description);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }
        [TestMethod]
        [TestCategory(nameof(IssueController.CreateIssue))]
        public async Task IssueController_CreateIssue_CitizenIsBlocked_ErrorNo208()
        {
            // Arrange
            IssueCreateDTO mockIssueDTO = new()
            {
                CitizenId = 5,
                Description = "Der er graffiti p? min v?g",
                MunicipalityId = 1,
                SubCategoryId = 2,
                LocationPlaceHolder = new Location(57.012218, 9.994330),
                Address = "Alfred Nobels Vej 27, 9200 Aalborg, Danmark"
            };
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            int ErrorNo = (int)ResponseErrors.CitizenIsBlocked;

            // Act
            var result = await controller.CreateIssue(mockIssueDTO);

            // Assert                
            Assert.AreEqual(ErrorNo, result.Value.ErrorNo);
        }
        [TestMethod]
        [TestCategory(nameof(IssueController.CreateIssue))]
        public async Task IssueController_CreateIssue_CitizenDoesNotExist_ErrorNo200()
        {
            // Arrange
            IssueCreateDTO mockIssueDTO = new()
            {
                CitizenId = 50,
                Description = "Der er graffiti p? min v?g",
                MunicipalityId = 1,
                SubCategoryId = 2,
                LocationPlaceHolder = new Location(57.012218, 9.994330),
                Address = "Alfred Nobels Vej 27, 9200 Aalborg, Danmark"
            };
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            int ErrorNo = (int)ResponseErrors.CitizenDoesNotExist;

            // Act            
            var result = await controller.CreateIssue(mockIssueDTO);

            // Assert
            Assert.AreEqual(ErrorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.DeleteIssue))]
        public async Task IssueController_DeleteIssue_Successful()
        {
            // Arrange
            int issueId = 1;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Ensure issue exists before it is deleted
            var retrivedDTO = await controller.GetIssueDetails(issueId);
            var retrivedIssue = retrivedDTO.Value.Result;

            // Attempt to return an issue using a non-existant Id
            var result = await controller.DeleteIssue(issueId);
            using (var context = contextFactory.CreateDbContext())
            {
                var retrieveAttempt = context.Issues.FirstOrDefault(issue => issue.Id == issueId);
                // Assert

                // Verify that the attempt to find issue does not find the issue
                Assert.IsNotNull(retrivedIssue);
                Assert.IsNull(retrieveAttempt);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.DeleteIssue))]
        public async Task IssueController_DeleteIssue_ContainingReportsVerify_Successful()
        {
            // Arrange
            int issueId = 3;
            int citizenId = 2;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            // Act

            // Place a report and a verification on the issue
            var verifyResult = await controller.VerifyIssue(issueId, citizenId);
            var reportResult = await controller.ReportIssue(issueId, 1);


            // Delete the issue
            var result = await controller.DeleteIssue(issueId);
            using (var context = contextFactory.CreateDbContext())
            {
                var delVerif = context.Verifications.FirstOrDefault(verif => verif.IssueId == issueId);
                var delReport = context.Reports.FirstOrDefault(report => report.IssueId == issueId);

                // Assert

                // Verify that the verification and report is now deleted
                Assert.IsNull(delVerif);
                Assert.IsNull(delReport);

                // Verify that the function response is successful
                Assert.IsTrue(verifyResult.Value.IsSuccessful);
                Assert.IsTrue(reportResult.Value.IsSuccessful);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.VerifyIssue))]
        public async Task IssueController_VerifyIssue_Successful()
        {
            // Arrange
            int issueId = 1;
            int citizenId = 3;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            var beforeVerifs = await controller.GetIssueDetails(issueId);
            var result = await controller.VerifyIssue(issueId, citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                Verification verif = context.Verifications.FirstOrDefault(cit => cit.CitizenId == citizenId);

                // Assert

                // Verify that the issue-verification exists.
                Assert.IsNotNull(verif);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.ReportIssue))]
        public async Task IssueController_ReportIssue_Successful()
        {
            // Arrange
            int issueId = 2;
            int reportCategoryId = 1;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.ReportIssue(issueId, reportCategoryId);
            using (var context = contextFactory.CreateDbContext())
            {
                Report report = context.Reports.FirstOrDefault(issue => issue.IssueId == issueId);

                // Assert

                // Verify that the report exists and it has one on its instances.
                Assert.IsNotNull(report);
                Assert.IsTrue(report.TypeCounter == 1);
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.ReportIssue))]
        public async Task IssueController_ReportIssue_IncreaseTypeCounter_Successful()
        {
            // Arrange
            int issueId = 3;
            int reportCategoryId = 1;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Perform two reports of the same category on the same issue
            var result1 = await controller.ReportIssue(issueId, reportCategoryId);
            var result2 = await controller.ReportIssue(issueId, reportCategoryId);
            using (var context = contextFactory.CreateDbContext())
            {
                Report report = context.Reports.FirstOrDefault(issue => issue.IssueId == issueId);

                // Assert

                //Verify that a report could be found
                Assert.IsNotNull(report);

                //Verify that the issue now has two reports on it.
                Assert.IsTrue(report.TypeCounter == 2);
                Assert.IsTrue(result1.Value.IsSuccessful);
                Assert.IsTrue(result2.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssueStatus))]
        public async Task IssueController_UpdateIssueStatus_Successful()
        {
            // Arrange
            int issueId1 = 1;
            int issueId2 = 2;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Update an issue's status to the next status (Created -> Approved & Approved -> Resolved) 
            var result1 = await controller.UpdateIssueStatus(issueId1, 2);
            var result2 = await controller.UpdateIssueStatus(issueId2, 3);
            using (var context = contextFactory.CreateDbContext())
            {
                // Get the state of the issue just updated.
                int stateIss1 = context.Issues.FirstOrDefault(issue => issue.Id == issueId1).IssueStateId;
                int stateIss2 = context.Issues.FirstOrDefault(issue => issue.Id == issueId2).IssueStateId;

                // Assert

                // Verify that the issue's new status is the expected new one.
                Assert.IsTrue(stateIss1 == 2);
                Assert.IsTrue(stateIss2 == 3);
                Assert.IsTrue(result1.Value.IsSuccessful);
                Assert.IsTrue(result2.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetIssueDetails))]
        public async Task IssueController_GetIssueDetails_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            int testId = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Attempt to return issue using a non-existant Id
            var getResult = await controller.GetIssueDetails(testId);

            using (var context = contextFactory.CreateDbContext())
            {

                // Assert

                // Verify that the function returns the relevant error code.
                Assert.AreEqual(notExistCode, getResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssue))]
        public async Task IssueController_UpdateIssue_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            int testId = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            IEnumerable<WASPUpdate> update = new List<WASPUpdate>()
                {
                    new()
                    {
                        Name = "Description",
                        Value = "Der er lort p? fortorvet"
                    }
                };

            // Act

            // Attempt to update issue using a non-existant Id
            var updateResult = await controller.UpdateIssue(testId, update);
            using (var context = contextFactory.CreateDbContext())
            {

                // Assert

                // Verify that the function returns the relevant error code.
                Assert.AreEqual(notExistCode, updateResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssueStatus))]
        public async Task IssueController_UpdateIssueStatus_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            int testId = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Attempt to update issue state using a non-existant Id
            var statusResult = await controller.UpdateIssueStatus(testId, 3);
            using (var context = contextFactory.CreateDbContext())
            {

                // Assert

                // Verify that the function returns the relevant error code.
                Assert.AreEqual(notExistCode, statusResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.VerifyIssue))]
        public async Task IssueController_VerifyIssue_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            int testId = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Attempt to verify issue using a non-existant Id
            var verifResult = await controller.VerifyIssue(testId, 3);
            using (var context = contextFactory.CreateDbContext())
            {

                // Assert

                // Verify that the function returns the relevant error code.
                Assert.AreEqual(notExistCode, verifResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.ReportIssue))]
        public async Task IssueController_ReportIssue_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            int testId = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Attempt to report issue using a non-existant Id
            var reportResult = await controller.ReportIssue(testId, 1);
            using (var context = contextFactory.CreateDbContext())
            {

                // Assert

                // Verify that the function returns the relevant error code.
                Assert.AreEqual(notExistCode, reportResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.DeleteIssue))]
        public async Task IssueController_DeleteIssue_IssueDoesNotExist_ErrorNo104()
        {
            // Arrange
            int testId = 999;
            int notExistCode = (int)ResponseErrors.IssueDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Attempt to delete issue using a non-existant Id
            var deleteResult = await controller.DeleteIssue(testId);
            using (var context = contextFactory.CreateDbContext())
            {

                // Assert

                // Verify that the function returns the relevant error code.
                Assert.AreEqual(notExistCode, deleteResult.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssueStatus))]
        public async Task IssueController_UpdateIssueStatus_IssueStatusDoesNotExist_ErrorNo104()
        {
            // Arrange
            int issueId = 1;
            int notExistCode = (int)ResponseErrors.IssueStateDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            // Attempt to update an issue's status to one that does not exist
            var result = await controller.UpdateIssueStatus(issueId, 69);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                // Verify the function returns the relevant error code
                Assert.AreEqual(notExistCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.CreateIssue))]
        public async Task IssueController_CreateIssue_NoSuchSubCategory_ErrorNo101()
        {
            // Arrange
            int mockSubCategory = 123;
            int categoryErrorCode = (int)ResponseErrors.SubCategoryDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Create a IssueDTO with a subcategory that does not exist.
            IssueCreateDTO mockIssueDTO = new()
            {
                CitizenId = 3,
                Description = "Nogen smider flasker udenfor",
                MunicipalityId = 2,
                SubCategoryId = mockSubCategory,
                LocationPlaceHolder = new Location(55.22321, 10.0023)
            };

            // Act

            // Update the issue with an invalid Subcategory Id
            var result = await controller.CreateIssue(mockIssueDTO);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that the function returns the relevant error code
                Assert.AreEqual(categoryErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssue))]
        public async Task IssueController_UpdateIssue_NoSuchSubCategory_ErrorNo101()
        {
            // Arrange
            int issueId = 2;
            int mockSubCategory = 123;
            int categoryErrorCode = (int)ResponseErrors.SubCategoryDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            //Create a WASPupdate with a subcategory that does not exist.
            IEnumerable<WASPUpdate> mockUpdate = new List<WASPUpdate>()
                {
                    new ()
                    {
                        Name = "SubCategoryId",
                        Value = mockSubCategory
                    }
                };

            // Act

            //Update the issue with an invalid Subcategory Id
            var result = await controller.UpdateIssue(issueId, mockUpdate);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that the function returns the relevant error code
                Assert.AreEqual(categoryErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssue))]
        public async Task IssueController_UpdateIssue_WASPUpdateBadFormat_ErrorNo50()
        {
            // Arrange
            int issueId = 3;
            int WASPErrorCode = (int)ResponseErrors.WASPUpdateListBadFormat;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);
            //Create a WASP update with a property that doesn't exist
            IEnumerable<WASPUpdate> mockUpdate = new List<WASPUpdate>()
                {
                    new ()
                    {
                        Name = "UNKNOWNPROP",
                        Value = null
                    }
                };

            // Act

            //Update an issue with this (invalid) property change
            var result = await controller.UpdateIssue(issueId, mockUpdate);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that attemping a second verification returns the relevant error
                Assert.AreEqual(WASPErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.UpdateIssueStatus))]
        public async Task IssueController_UpdateIssueStatus_DisallowedIssueStateChange_ErrorNo105()
        {
            // Arrange
            int issueId1 = 1;
            int issueId2 = 2;
            int issueId3 = 3;
            int issueId4 = 4;
            int disallowedCode = (int)ResponseErrors.DisallowedIssueStateChange;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            //Attempt a status update that does not follow the normal convention (i.e going reverse or the same value)
            var result1 = await controller.UpdateIssueStatus(issueId1, 1);
            var result2 = await controller.UpdateIssueStatus(issueId2, 1);
            var result3 = await controller.UpdateIssueStatus(issueId3, 2);
            var result4 = await controller.UpdateIssueStatus(issueId4, 2);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that the function returns the relevant error code
                Assert.AreEqual(disallowedCode, result1.Value.ErrorNo);
                Assert.AreEqual(disallowedCode, result2.Value.ErrorNo);
                Assert.AreEqual(disallowedCode, result3.Value.ErrorNo);
                Assert.AreEqual(disallowedCode, result4.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.VerifyIssue))]
        public async Task IssueController_VerifyIssue_IssueCannotBeVerifiedByItsCreator_ErrorNo108()
        {
            // Arrange
            int issueId = 2;
            int citizenId = 2;
            int creatorErrorCode = (int)ResponseErrors.IssueCannotBeVerifiedByItsCreator;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            //Attempt to update an issue's status to one that does not exist
            var result = await controller.VerifyIssue(issueId, citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify the function returns the relevant error code
                Assert.AreEqual(creatorErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.VerifyIssue))]
        public async Task IssueController_VerifyIssue_IssueAlreadyVerifiedByThisCitizen_ErrorNo106()
        {
            // Arrange
            int issueId = 3;
            int citizenId = 4;
            int creatorErrorCode = (int)ResponseErrors.IssueAlreadyVerifiedByThisCitizen;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            //Verify an issue once
            var resultFirst = await controller.VerifyIssue(issueId, citizenId);

            //Attempt a verify again by the same civilian Id
            var resultSecond = await controller.VerifyIssue(issueId, citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that verification works the first time
                Assert.IsTrue(resultFirst.Value.IsSuccessful);

                //Verify that attemping a second verification returns the relevant error
                Assert.AreEqual(creatorErrorCode, resultSecond.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.VerifyIssue))]
        public async Task IssueController_VerifyIssue_CitizenDoesNotExist_ErrorNo200()
        {
            // Arrange
            int issueId = 2;
            int mockCitizen = -5;
            int citizenErrorCode = (int)ResponseErrors.CitizenDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            //Report using a category Id that is not indexed
            var result = await controller.VerifyIssue(issueId, mockCitizen);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that attemping a second verification returns the relevant error
                Assert.AreEqual(citizenErrorCode, result.Value.ErrorNo);
            }
        }
        [TestMethod]
        [TestCategory(nameof(IssueController.VerifyIssue))]
        public async Task IssueController_VerifyIssue_CitizenIsBlocked_ErrorNo208()
        {
            // Arrange
            int issueId = 2;
            int citizenId = 5;
            int citizenErrorCode = (int)ResponseErrors.CitizenIsBlocked;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            //Report using a category Id that is not indexed
            var result = await controller.VerifyIssue(issueId, citizenId);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that attemping a second verification returns the relevant error
                Assert.AreEqual(citizenErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.ReportIssue))]
        public async Task IssueController_ReportIssue_ReportCategoryDoesNotExist_ErrorNo107()
        {
            // Arrange
            int issueId = 2;
            int mockCategory = 101;
            int categoryErrorCode = (int)ResponseErrors.ReportCategoryDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act

            //Report using a category Id that is not indexed
            var result = await controller.ReportIssue(issueId, mockCategory);
            using (var context = contextFactory.CreateDbContext())
            {
                // Assert

                //Verify that attemping a second verification returns the relevant error
                Assert.AreEqual(categoryErrorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfReportCategories))]
        public async Task MunicipalityController_GetListOfReportCategories_Successful()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            var result = await controller.GetListOfReportCategories();
            using (var context = contextFactory.CreateDbContext())
            {

                // Return the length of the issue-list in the context
                var expectedList = context.ReportCategories.Count();

                // Assert

                // Verify that the length of the list is equal to the context issue-list length
                Assert.AreEqual(expectedList, result.Value.Result.Count());
                Assert.IsTrue(result.Value.IsSuccessful);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfReports))]
        public async Task IssueController_GetListOfReports_MunicipalityDoesNotExist()
        {
            // Arrange
            int municipalityId = 10;            
            int errorCode = (int)ResponseErrors.MunicipalityDoesNotExist;
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act            
            var result = await controller.GetListOfReports(municipalityId);

            // Assert                
            using (var context = contextFactory.CreateDbContext())
            {
                Assert.AreEqual(errorCode, result.Value.ErrorNo);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueController.GetListOfReports))]
        public async Task IssueController_GetListOfReports_Success()
        {
            // Arrange
            int municipalityId = 1;            
            var contextFactory = new MockHiveContextFactory();
            IssueController controller = new(contextFactory);

            // Act            
            var result = await controller.GetListOfReports(municipalityId);

            // Assert                
            using (var context = contextFactory.CreateDbContext())
            {
                Assert.IsTrue(result.Value.IsSuccessful);
                Assert.IsInstanceOfType(result.Value.Result, typeof(IEnumerable<IssueReportDTO>));
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueDetailsDTO))]
        public void IssueController_IssueDetailsDTO_LocationPlaceHolder_CorrectValues()
        {
            // Arrange
            var contextFactory = new MockHiveContextFactory();
            var location = new Location()
            {
                Latitude = 1,
                Longitude = 1
            };

            // Act 
            // Assert
            using (var context = contextFactory.CreateDbContext())
            {
                var dto = new IssueDetailsDTO(context.Issues.FirstOrDefault());
                dto.LocationPlaceHolder = location;

                Assert.AreEqual(location.Latitude, dto.LocationPlaceHolder.Latitude);
                Assert.AreEqual(location.Longitude, dto.LocationPlaceHolder.Longitude);                
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssuesOverviewDTO))]
        public void IssueController_IssueOverviewDTO_LocationPlaceHolder_CorrectValues()
        {
            // Arrange
            var contextFactory = new MockHiveContextFactory();
            var location = new Location(1, 1);

            // Act 
            // Assert
            using (var context = contextFactory.CreateDbContext())
            {
                var dto = new IssuesOverviewDTO(context.Issues.FirstOrDefault());
                dto.LocationPlaceHolder = location;

                Assert.AreEqual(location.Latitude, dto.LocationPlaceHolder.Latitude);
                Assert.AreEqual(location.Longitude, dto.LocationPlaceHolder.Longitude);
            }
        }

        [TestMethod]
        [TestCategory(nameof(CategoryListDTO))]
        public void IssueController_CategoryListDTO_CorrectValues()
        {
            // Arrange
            var contextFactory = new MockHiveContextFactory();            

            // Act 
            // Assert
            using (var context = contextFactory.CreateDbContext())
            {
                var category = context.Categories.FirstOrDefault();
                var dto = new CategoryListDTO(category);                

                Assert.AreEqual(category.Id, dto.Id);
                Assert.AreEqual(category.Name, dto.Name);
                Assert.AreEqual(category.SubCategories.Count, dto.SubCategories.Count);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueDetailsDTO))]
        public void IssueController_IssueDetailsDTO_CorrectValues()
        {
            // Arrange
            var contextFactory = new MockHiveContextFactory();

            // Act 
            // Assert
            using (var context = contextFactory.CreateDbContext())
            {
                var issue = context.Issues.FirstOrDefault();
                var dto = new IssueDetailsDTO(issue);                

                Assert.AreEqual(issue.CitizenId, dto.CitizenId);
                Assert.AreEqual(issue.DateCreated, dto.DateCreated);
                Assert.AreEqual(issue.DateEdited, dto.DateEdited);
                Assert.AreEqual(issue.Address, dto.Address);
                Assert.AreEqual(issue.Picture1, dto.Picture1);
                Assert.AreEqual(issue.Picture2, dto.Picture2);
                Assert.AreEqual(issue.Picture3, dto.Picture3);
                Assert.AreEqual(issue.Category.Name, dto.Category.Name);
                Assert.AreEqual(issue.SubCategory.Name, dto.SubCategory.Name);
                Assert.AreEqual(issue.Municipality.Name, dto.Municipality.Name);
                Assert.AreEqual(issue.IssueState.Name, dto.IssueState.Name);
                Assert.AreEqual(issue.MunicipalityResponses.Count, dto.MunicipalityResponses.Count);
                Assert.AreEqual(issue.Verifications.Count, dto.IssueVerificationCitizenIds.Count);
            }
        }

        [TestMethod]
        [TestCategory(nameof(IssueReportDTO))]
        public void IssueController_IssueReportDTO_CorrectValues()
        {
            // Arrange
            var contextFactory = new MockHiveContextFactory();

            // Act 
            // Assert
            using (var context = contextFactory.CreateDbContext())
            {
                var issue = context.Issues.FirstOrDefault();
                var dto = new IssueReportDTO(issue);

                Assert.AreEqual(issue.Id, dto.Id);
                Assert.AreEqual(issue.Description, dto.Description);
                Assert.AreEqual(issue.MunicipalityId, dto.MunicipalityId);
                Assert.AreEqual(issue.Citizen.Id, dto.Citizen.Id);
                Assert.AreEqual(issue.Reports.Count, dto.Reports.Count);
            }
        }
    }
}
