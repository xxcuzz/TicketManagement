using System.Collections.Generic;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.UnitTests
{
    [TestFixture]
    public sealed class LayoutValidatorTests
    {
        [Test]
        public void ValidateLayout_ReturnTrue_When_LayoutDescriptionIsUniqueInVenue()
        {
            // Arrange
            var layout = new Layout
            {
                Description = "Unique",
            };

            var layouts = new List<Layout>
            {
                new Layout
                {
                    VenueId = 1,
                    Description = "UniqueLayoutUnitTest",
                },
            };

            // Act
            var result = LayoutValidator.IsLayoutDescriptionUniqueInVenue(layout, layouts);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateLayout_ReturnFalse_When_LayoutDescriptionIsNotUniqueInVenue()
        {
            // Arrange
            var layout = new Layout
            {
                Description = "Non-UniqueLayoutUnitTest",
            };
            var layouts = new List<Layout>
            {
                new Layout
                {
                    VenueId = 1,
                    Description = "Non-UniqueLayoutUnitTest",
                },
            };

            // Act
            var result = LayoutValidator.IsLayoutDescriptionUniqueInVenue(layout, layouts);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
