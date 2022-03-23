using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipesWebApp.Data;
using RecipesWebApp.Data.Models;
using System;
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

            //Console.WriteLine(data.Database.EnsureDeleted());

            //data.Database.EnsureCreated();

            data.Database.Migrate();

            SeedMealTypes(data);

            //SeedPortions();

            return app;
        }

        //private static void SeedPortions(RecipesDbContext data)
        //{
        //    if (data.Portions.Any())
        //    {
        //        return;
        //    }

        //    data.Portions.AddRange(new[]
        //    {
        //        new MealType { Name = "1" },
        //        new MealType { Name = "2" },
        //        new MealType { Name = "3" },
        //        new MealType { Name = "4" },
        //        new MealType { Name = "5" },
        //        new MealType { Name = "6" },
        //        new MealType { Name = "7" },
        //    });

        //    data.SaveChanges();
        //}

        private static void SeedMealTypes(RecipesDbContext data)
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
