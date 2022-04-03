﻿namespace RecipesWebApp.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RecipesWebApp.Data;
    using RecipesWebApp.Models;
    using RecipesWebApp.Models.Recipes;
    using RecipesWebApp.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly RecipesDbContext data;

       // private readonly ILogger<HomeController> _logger;

        public HomeController(
            IStatisticsService statistics,
           // ILogger<HomeController> logger,
            RecipesDbContext data)
        {
            this.statistics = statistics;
           // _logger = logger;
            this.data = data;
        }

        public IActionResult Index()
        {
            var recipes = this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    CookingTime = r.CookingTime,
                    ImageUrl = r.ImageUrl,
                    MealType = r.MealType.Name
                })
                .Take(6)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(recipes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
