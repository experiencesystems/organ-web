using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganWeb.Areas.Sistema.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers.Tests
{
    [TestClass()]
    public class PlantioControllerTests
    {
        [TestMethod()]
        public async Task IndexTest()
        {// Arrange
            PlantioController controller = new PlantioController();
            // Act
            var result = await controller.Index() as ActionResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}