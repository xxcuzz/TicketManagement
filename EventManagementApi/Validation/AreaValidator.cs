using System.Collections.Generic;
using System.Linq;
using EventManagementApi.EntitiesDTO;
using TicketManagement.DataAccess.Entities;

namespace EventManagementApi.Validation
{
    public static class AreaValidator
    {
        /// <summary>
        /// Validates <paramref name="item"/> with business logic.
        /// </summary>
        /// <returns>True if validation succeed.</returns>
        public static bool Validate(Area item, Layout layout, IEnumerable<AreaDto> areas)
        {
            if (layout == null)
            {
                return false;
            }

            return IsAreaDescriptionUniqueInLayout(item, areas);
        }

        public static bool IsAreaDescriptionUniqueInLayout(Area item, IEnumerable<AreaDto> areas)
        {
            areas = areas.Where(a => a.Description == item.Description);
            return !areas.Any();
        }
    }
}
