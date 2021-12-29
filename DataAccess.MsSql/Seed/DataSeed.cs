using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models.Models;

namespace DataAccess.MsSql.Seed
{
    public static class DataSeed
    {
        public static async Task SeedData(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IDbContext dbContext)
        {
            string[] roleNames = { "Admin", "Moderator", "User" };

            foreach (string roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new Role() { Name = roleName});
            }

            if (!userManager.Users.Any())
            {
                var users = new List<User>()
                {
                    new User
                    {
                        UserName = "Admin",
                        Email = "admin@test.com"
                    },
                    new User
                    {
                        UserName = "Moderator",
                        Email = "moderator@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                await userManager.AddToRoleAsync(users.FirstOrDefault(x => x.UserName == "Admin"), "Admin");
                await userManager.AddToRoleAsync(users.FirstOrDefault(x => x.UserName == "Moderator"), "Moderator");
            }

            if (!dbContext.Flights.Any())
            {
                var flights = new Flight[] {
                    new Flight
                    {
                        DepartureCityId = 1,
                        ArrivalCityId = 2,
                        DepartureTime = new DateTime(2021, 12, 29, 12, 22, 10),
                        ArrivalTime = new DateTime(2021, 12, 29, 12, 33, 10)
                    },
                    new Flight
                    {
                        DepartureCityId = 3,
                        ArrivalCityId = 4,
                        DepartureTime = new DateTime(2021, 12, 29, 15, 22, 10),
                        ArrivalTime = new DateTime(2021, 12, 29, 15, 44, 10)
                    },
                };

                dbContext.Flights.AddRange(flights);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
