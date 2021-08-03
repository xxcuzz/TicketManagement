using System;
using System.Collections.Generic;
using System.Linq;
using EventManagementApi.EntitiesDTO;
using TicketManagement.DataAccess.Entities;

namespace EventManagementApi.Validation
{
    public static class EventValidator
    {
        /// <summary>
        /// Validates <paramref name="item"/> with business logic.
        /// </summary>
        /// <returns>True if validation succeed.</returns>
        public static bool Validate(Event item, Layout layout, IEnumerable<Event> events, IEnumerable<SeatDto> seats)
        {
            if (layout == null)
            {
                return false;
            }

            if (!IsEventStartLessThanEventEnd(item.EventStart, item.EventEnd))
            {
                return false;
            }

            if (!CheckEventPastTime(item.EventStart))
            {
                return false;
            }

            if (!CheckSeats(seats))
            {
                return false;
            }

            if (!CheckSameTimeForSameVenue(item, events))
            {
                return false;
            }

            return true;
        }

        public static bool IsEventStartLessThanEventEnd(DateTime start, DateTime end)
        {
            if (start <= end)
            {
                return true;
            }

            return false;
        }

        public static bool CheckEventPastTime(DateTime time)
        {
            if (time < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public static bool CheckSameTimeForSameVenue(Event item, IEnumerable<Event> events)
        {
            foreach (var e in events.ToArray())
            {
                if (e.Id == item.Id)
                {
                    continue;
                }

                if (!(item.EventEnd > e.EventStart && item.EventStart > e.EventEnd) &&
                    !(item.EventEnd < e.EventStart && item.EventStart < e.EventStart))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckSeats(IEnumerable<SeatDto> seats)
        {
            if (seats.Any())
            {
                return true;
            }

            return false;
        }
    }
}
