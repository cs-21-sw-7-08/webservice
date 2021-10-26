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

namespace WASP_UnitTests
{
    [TestClass]
    public class IssueControllerUnitTests
    {
        [TestMethod]
        public async Task GetListOfIssues()
        {
            // Arrange
            var controller = new IssueController(new HiveContextFactory());            

            // Act
            var response = await controller.GetListOfIssues(new IssuesOverviewFilter());            

            // Assert            
            Assert.IsTrue(true);
        }        
    }
}
