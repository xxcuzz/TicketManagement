using System.Collections.Generic;
using NUnit.Framework;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.UnitTests
{
    [TestFixture]
    public class AreaValidationTests
    {
        [Test]
        public void ValidateArea_ReturnFalse_When_AreaDescriptionIsNotUniqueInLayout()
        {
            // Arrange
            var area = new Area { LayoutId = 1, Description = "StandartAreaUnitTest", CoordX = 0, CoordY = 0 };

            var areas = new List<AreaDto>
            {
                new AreaDto
                {
                    Description = "StandartAreaUnitTest",
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                },
            };

            // Act
            var result = AreaValidator.IsAreaDescriptionUniqueInLayout(area, areas);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateArea_ReturnTrue_When_AreaDescriptionIsUniqueInLayout()
        {
            // Arrange
            var area = new Area { LayoutId = 1, Description = "StandartAreaUnitTest", CoordX = 0, CoordY = 0 };

            var areas = new List<AreaDto>
            {
                new AreaDto
                {
                    Description = "UniqueAreaDescriptionUnitTest",
                    LayoutId = 1,
                    CoordX = 1,
                    CoordY = 1,
                },
            };

            // Act
            var result = AreaValidator.IsAreaDescriptionUniqueInLayout(area, areas);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
