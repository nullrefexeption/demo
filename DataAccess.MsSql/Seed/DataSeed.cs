using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models.Models;

namespace DataAccess.MsSql.Seed
{
    public static class DataSeed
    {
        public static async Task SeedData(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
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
                
        }
    }
}
