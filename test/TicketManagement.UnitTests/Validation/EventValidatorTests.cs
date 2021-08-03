using System;
using System.Collections.Generic;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.UnitTests
{
    [TestFixture]
    public class EventValidatorTests
    {
        [Test]
        public void CheckEventPastTime_ReturnFalse_When_CreateEventInThePast()
        {
            // Arrange
            var dt = DateTime.Now.AddHours(-1);

            // Act
            var result = EventValidator.CheckEventPastTime(dt);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckEventPastTime_ReturnTrue_When_CreateEventInFuture()
        {
            // Arrange
            var dt = DateTime.Now.AddHours(1);

            // Act
            var result = EventValidator.CheckEventPastTime(dt);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateEvent_ReturnFalse_When_LaterEventTimeIntersectsWithTheTimeOfExistingEvent()
        {
            // Arrange
            var event1 = CreateEventsWithTimeOffset(10, 10);
            var events = CreateEventWithDatetimeNow();

            // Act
            var result = EventValidator.CheckSameTimeForSameVenue(event1, events);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CreateEvent_ReturnFalse_When_EarlierTimeIntersectsWithTheTimeOfExistingEvent()
        {
            // Arrange
            var event1 = CreateEventsWithTimeOffset(-10, -10);
            var events = CreateEventWithDatetimeNow();

            // Act
            var result = EventValidator.CheckSameTimeForSameVenue(event1, events);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CreateEvent_ReturnFalse_When_EventTimeIsInsideTimeOfExistingEvent()
        {
            var event1 = CreateEventsWithTimeOffset(10, -10);
            var events = CreateEventWithDatetimeNow();

            // Act
            var result = EventValidator.CheckSameTimeForSameVenue(event1, events);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CreateEvent_ReturnFalse_When_EventTimeContainsTimeOfExistingEvent()
        {
            // Arrange
            var event1 = CreateEventsWithTimeOffset(-10, 10);

            var events = CreateEventWithDatetimeNow();

            // Act
            var result = EventValidator.CheckSameTimeForSameVenue(event1, events);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CreateEvent_ReturnTrue_When_EventTimeDoesNotIntersectsWithTheTimeOfExistingEvent()
        {
            // Arrange
            var event1 = new Event
            {
                Description = "UnitTestDescription",
                LayoutId = 1,
                Name = "UnitTestName",
                EventStart = new DateTime(1999, 01, 01, 01, 01, 01),
                EventEnd = new DateTime(2000, 01, 01, 03, 01, 01),
            };
            var events = CreateEventWithDatetimeNow();

            // Act
            var result = EventValidator.CheckSameTimeForSameVenue(event1, events);

            // Assert
            Assert.IsTrue(result);
        }

        private static Event CreateEventsWithTimeOffset(int startTimeMinutesOffset, int endTimeMinutesOffset)
        {
            return new Event
            {
                Description = "UnitTestDescription",
                LayoutId = 1,
                Name = "UnitTestName",
                EventStart = DateTime.Now.AddMinutes(startTimeMinutesOffset),
                EventEnd = DateTime.Now.AddHours(2).AddMinutes(endTimeMinutesOffset),
            };
        }

        public static IEnumerable<Event> CreateEventWithDatetimeNow()
        {
            var events = new List<Event>
            {
                new Event
                {
                    Id = -1,
                    Description = "UnitTestDescription",
                    LayoutId = 1,
                    Name = "UnitTestName",
                    EventStart = DateTime.Now,
                    EventEnd = DateTime.Now.AddHours(2),
                },
            };
            return events;
        }
    }
}