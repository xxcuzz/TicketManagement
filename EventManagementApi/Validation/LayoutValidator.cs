using System.Collections.Generic;
using System.Linq;
using TicketManagement.DataAccess.Entities;

namespace EventManagementApi.Validation
{
    public static class LayoutValidator
    {
        /// <summary>
        /// Validates <paramref name="item"/> with business logic.
        /// </summary>
        /// <returns> True if <paramref name="item"/>  is valid. </returns>
        public static bool Validate(Layout item, Venue venue, IEnumerable<Layout> layouts)
        {
            if (venue == null)
            {
                return false;
            }

            return IsLayoutDescriptionUniqueInVenue(item, layouts);
        }

        public static bool IsLayoutDescriptionUniqueInVenue(Layout item, IEnumerable<Layout> layouts)
        {
            layouts = layouts.Where(l => l.Description == item.Description);
            return !layouts.Any();
        }
    }
}
