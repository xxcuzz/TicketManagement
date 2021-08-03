using System.Collections.Generic;
using System.Linq;
using EventManagementApi.EntitiesDTO;
using TicketManagement.DataAccess.Entities;

namespace EventManagementApi.Validation
{
    public static class VenueValidator
    {
        /// <summary>
        /// Validates <paramref name="item"/> with business logic.
        /// </summary>
        /// <returns>True if validation succeed.</returns>
        public static bool Validate(VenueDto item, IEnumerable<Venue> venues)
        {
            if (venues == null)
            {
                return false;
            }

            return IsVenueDescriptionIsUnique(item, venues);
        }

        public static bool IsVenueDescriptionIsUnique(VenueDto item, IEnumerable<Venue> venues)
        {
            return venues.FirstOrDefault(v => v.Description != item.Description) != null;
        }
    }
}
