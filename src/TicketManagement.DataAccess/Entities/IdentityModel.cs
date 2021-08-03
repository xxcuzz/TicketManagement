using Microsoft.AspNetCore.Identity;

namespace TicketManagement.DataAccess.Entities
{
    public class IdentityModel : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int Timezone { get; set; }

        public string Language { get; set; }
    }
}
