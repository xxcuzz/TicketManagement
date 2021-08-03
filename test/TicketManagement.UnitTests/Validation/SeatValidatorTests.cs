using System.Collections.Generic;
using NUnit.Framework;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.UnitTests
{
    [TestFixture]
    public class SeatValidatorTests
    {
        [Test]
        public void ValidateSeat_ReturnFalse_When_RowAndNumberIsNotUniqueInArea()
        {
            // Arrange
            var seat = new Seat { AreaId = 1, Row = 1, Number = 1 };
            var seats = GetListOfSeats();

            // Act
            var result = SeatValidator.IsNumberAndRowSeatAreUniqueInArea(seat, seats);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateSeat_ReturnTrue_When_NumberIsUniqueInArea()
        {
            // Arrange
            var seat = new Seat { AreaId = 1, Row = 1, Number = 2 };
            var seats = GetListOfSeats();

            // Act
            var result = SeatValidator.IsNumberAndRowSeatAreUniqueInArea(seat, seats);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateSeat_ReturnTrue_When_RowIsUniqueInArea()
        {
            // Arrange
            var seat = new Seat { AreaId = 1, Row = 2, Number = 1 };
            var seats = GetListOfSeats();

            // Act
            var result = SeatValidator.IsNumberAndRowSeatAreUniqueInArea(seat, seats);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateSeat_ReturnTrue_When_RowAndNumberIsUniqueInArea()
        {
            // Arrange
            var seat = new Seat { AreaId = 1, Row = 2, Number = 2 };
            var seats = GetListOfSeats();

            // Act
            var result = SeatValidator.IsNumberAndRowSeatAreUniqueInArea(seat, seats);

            // Assert
            Assert.IsTrue(result);
        }

        private static List<SeatDto> GetListOfSeats()
        {
            var seats = new List<SeatDto>
            {
                new SeatDto
                {
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
            };

            return seats;
        }
    }
}
