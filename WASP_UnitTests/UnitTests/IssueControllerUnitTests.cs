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
            MockHiveContextFactory contextFactory = new();
            IssueController controller = new(contextFactory);

            // Act
            using (var context = contextFactory.CreateDbContext())
            {
                int issueID = 2;
                var expectedIssue = context.Issues.FirstOrDefault(issue => issue.Id == issueID);
                var response = await controller.GetIssueDetails(issueID);
                var result = response.Value.Result;

                // Assert
                Assert.IsNotNull(result);
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
                var expectedList = context.Issues.Count<Issue>();
                var result = await controller.GetListOfIssues(new IssuesOverviewFilter());
                var response = result.Value;

                // Assert
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
            string altDescription = "Vejen er blevet beskidt";
            /*using (var context = contextFactory.CreateDbContext())
            {
                Issue oldIssue = context.Issues.FirstOrDefault(x => x.Id == issueID);
                Issue newIssue = oldIssue;
                newIssue.Description = altDescription;
                var result = await controller.UpdateIssue(newIssue);
                var acquiredIssue = controller.GetIssueDetails(issueID);
                

                // Assert
                Assert.AreEqual(acquiredIssue.Result.Value.Result.Description, altDescription);
            }*/
        }
    }
}
