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
            using (var context = contextFactory.CreateDbContext())
            {
                //Get an issue with a given ID from the context
                var expectedIssue = context.Issues.FirstOrDefault(issue => issue.Id == issueID);

                //Acquire an issue with a given ID using the controller function
                var response = await controller.GetIssueDetails(issueID);
                var result = response.Value.Result;

                // Assert

                //Check if the result is not null
                Assert.IsNotNull(result);

                //Check if the issue info is identical for both the context- and controller result
                Assert.AreEqual(expectedIssue.Id, result.Id);
                Assert.AreEqual(expectedIssue.Description, result.Description);
            }
        }

        [TestMethod]
        public async Task GetListOfIssues()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            using (var context = contextFactory.CreateDbContext())
            {

                //Return the length of the issue-list in the context
                var expectedList = context.Issues.Count();

                //Return the list of issues using the controller function
                var result = await controller.GetListOfIssues(new IssuesOverviewFilter());
                var response = result.Value;

                // Assert

                //Verify that the length of the list is equal to the context issue-list length.
                Assert.IsTrue(expectedList == response.Result.Count());
            }
        }

        [TestMethod]
        public async Task UpdateIssueInformation()
        {
            // Arrange
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);
            int issueID = 1;

            // Act
            using (var context = contextFactory.CreateDbContext())
            {
                IEnumerable<WASPUpdate> update = new List<WASPUpdate>()
                {
                    new()
                    {
                        Name = "Description",
                        Value = "Vejen er blevet beskidt"
                    }
                };

                // Get the issue (and description) from the context
                var oldIssue = context.Issues.FirstOrDefault(x => x.Id == issueID);
                string oldDesc = oldIssue.Description;
                // Acquire the same issue, using the database controller.
                var acquiredIssue = await controller.GetIssueDetails(issueID);
                var result = await controller.UpdateIssue(issueID, update);
                var newAcquiredIssue = controller.GetIssueDetails(issueID).Result;

                // Assert

                // Check if the description from the context and from using the controller are identical
                Assert.AreEqual(oldDesc, acquiredIssue.Value.Result.Description);

                // Check if the description has been updated after using UpdateIssue controller function
                Assert.AreEqual(newAcquiredIssue.Value.Result.Description, update.First().Value);
            }
        }
    }
}
