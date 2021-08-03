using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web
{
    public static class RoleInitializer
    {
        // In this method we will create default User roles
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Event Manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Event Manager"));
            }

            if (await roleManager.FindByNameAsync("Venue Manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Venue Manager"));
            }

            var eventManagerFirstName = "Event";
            var eventManagerSurname = "Manager";
            var eventManagerEmail = "eventManager@gmail.com";
            var eventManagerPassword = "Pass+w0rd";

            if (await userManager.FindByNameAsync(eventManagerEmail) == null)
            {
                var eventManager = new ApplicationUser { FirstName = eventManagerFirstName, Surname = eventManagerSurname, Email = eventManagerEmail, UserName = eventManagerEmail, Balance = 0.0M };
                var result = await userManager.CreateAsync(eventManager, eventManagerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(eventManager, "Event Manager");
                }
            }

            var venueManagerFirstName = "Venue";
            var venueManagerSurname = "Manager";
            var venueManagerEmail = "venueManager@gmail.com";
            var venueManagerPassword = "Pass+w0rd";

            if (await userManager.FindByNameAsync(venueManagerEmail) == null)
            {
                var venueManager = new ApplicationUser { FirstName = venueManagerFirstName, Email = venueManagerEmail, UserName = venueManagerEmail, Surname = venueManagerSurname, Balance = 0.0M };
                var result = await userManager.CreateAsync(venueManager, venueManagerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(venueManager, "Venue Manager");
                }
            }
        }
    }
}
