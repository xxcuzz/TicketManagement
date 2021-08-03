using System.Collections.Generic;
using System.Linq;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Validation
{
    public static class SeatValidator
    {
        /// <summary>
        /// Validates <paramref name="item"/> with business logic.
        /// </summary>
        /// <returns>True if validation succeed.</returns>
        public static bool Validate(Seat item, IEnumerable<SeatDto> seats, Area area)
        {
            // if area for item.AreaId doesn't exist
            if (area == null)
            {
                return false;
            }

            // if area doesn't contain any seats
            if (seats == null)
            {
                return true;
            }

            return IsNumberAndRowSeatAreUniqueInArea(item, seats);
        }

        public static bool IsNumberAndRowSeatAreUniqueInArea(Seat item, IEnumerable<SeatDto> seats)
        {
            seats = seats.Where(s => s.Number == item.Number && s.Row == item.Row);
            return !seats.Any();
        }
    }
}
