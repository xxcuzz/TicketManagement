using System.Collections.Generic;
using NUnit.Framework;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.UnitTests
{
    [TestFixture]
    public sealed class VenueValidatorTests
    {
        [Test]
        public void ValidateVenue_ReturnTrue_When_VenueDescriptionIsUnique()
        {
            // Arrange
            var venue = new VenueDto
            {
                Description = "Unique",
            };

            var venues = new List<Venue>
            {
                new Venue
                {
                    Description = "Non-unique",
                    Address = "address unitTest",
                    Phone = "22-33-44",
                },
            };

            // Act
            var result = VenueValidator.IsVenueDescriptionIsUnique(venue, venues);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateVenue_ReturnFalse_When_VenueDescriptionIsNonUnique()
        {
            // Arrange
            var venue = new VenueDto
            {
                Description = "Non-unique",
            };

            var venues = new List<Venue>
            {
                new Venue
                {
                    Description = "Non-unique",
                    Address = "address unitTest",
                    Phone = "22-33-44",
                },
            };

            // Act
            var result = VenueValidator.IsVenueDescriptionIsUnique(venue, venues);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
