using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipesWebApp.Data;
using RecipesWebApp.Data.Models;
using System.Linq;

namespace RecipesWebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<RecipesDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(RecipesDbContext data)
        {
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
    }
}
