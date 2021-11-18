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
    public class GeneralUnitTests
    {
        [TestMethod]
        [TestCategory("General")]
        public void General_WASPUpdate_Equals_True()
        {
            // Arrange
            WASPUpdate mockUpdate1 = new()
            {
                Name = "Description",
                Value = "Lygte i stkkr"
            };
            WASPUpdate mockUpdate2 = new()
            {
                Name = "Description",
                Value = "Lygte i stykker"
            };


            // Act
            // Assert
            Assert.IsTrue(WASPUpdate.Equals(mockUpdate1, mockUpdate2));
        }

        [TestMethod]
        [TestCategory("General")]
        public void General_WASPUpdate_Equals_False()
        {
            // Arrange
            WASPUpdate mockUpdate1 = new()
            {
                Name = "CitizenId",
                Value = "8"
            };
            WASPResponse mockResponse = new(3);


            // Act
            // Assert
            Assert.IsFalse(WASPUpdate.Equals(mockUpdate1, mockResponse));
        }
    }
}