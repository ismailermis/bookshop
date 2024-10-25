using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "İsmailE",
                    Email = "ismailermis@gmail.com",
                    UserName = "ismailermis@gmail.com",
                    Address = new Address
                    {
                        FirstName = "ismail",
                        LastName = "ermis",
                        Street = "cadde",
                        City = "istanbul",
                        State = "Turkey",
                        Zipcode = "3434"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}