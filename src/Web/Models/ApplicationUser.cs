using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int Timezone { get; set; }

        public string Language { get; set; }

        public decimal Balance { get; set; }
    }
}
