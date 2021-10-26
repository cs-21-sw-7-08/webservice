using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP;
using WASP.Models;
using WASP.DataAccessLayer;
using WASP.Controllers;
using System.Threading.Tasks;

namespace WASP_UnitTests
{
    [TestClass]
    public class IssuesTests
    {
        [TestMethod]
        public async Task Issue_FindIssue()
        {
            DataService dataService = new();
            int issueID = 1;
            int badIssueID = -5;
            Issue retrievedIssue = await dataService.GetIssueDetails(issueID);
            Issue retrievedIssue2 = await dataService.GetIssueDetails(badIssueID);

            Assert.IsNotNull(retrievedIssue);
            Assert.IsNull(retrievedIssue2);

        }

        [TestMethod]
        public async Task Issue_AddNewIssueAsync()
        {
            //Arrange
            DataService dataService = new();
            Issue testIssue = new() { 
                Name = "Glasskår på vejen", 
                Description = "Der er glasskår på vejen", 
                Id = 8, 
                Status = 0 
            };

            Issue retrievedIssue = await dataService.GetIssueDetails(testIssue.Id);
            Assert.IsNull(retrievedIssue);
            await dataService.CreateIssue(testIssue);
            //Assert.IsTrue(testIssue.Id, dataService.GetIssueDetails(testIssue.Id).Id);
            retrievedIssue = await dataService.GetIssueDetails(testIssue.Id);
            Assert.IsTrue(testIssue.Equals(retrievedIssue));

        }
    }
}
