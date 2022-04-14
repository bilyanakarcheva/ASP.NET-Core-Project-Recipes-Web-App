namespace RecipesWebApp.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedMealTypes(services);

            SeedAministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {

            var data = services.GetRequiredService<RecipesDbContext>();

            data.Database.Migrate();
        }

        private static void SeedMealTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<RecipesDbContext>();

            if (data.MealTypes.Any())
            {
                return;
            }

            data.MealTypes.AddRange(new[]
            {
                new MealType { Name = "Soup" },
                new MealType { Name = "Dessert" },
                new MealType { Name = "Main Course" },
                new MealType { Name = "Salad" },
            });

            data.SaveChanges();
        }

        private static void SeedAministrator(
            IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminCredentials = "admin@tastybits.com";
                    const string adminPassword = "adminPass123";

                    var user = new IdentityUser
                    {
                        Email = adminCredentials,
                        UserName = adminCredentials
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
