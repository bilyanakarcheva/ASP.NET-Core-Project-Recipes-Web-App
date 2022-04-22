﻿namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Services.Recipes;
    using RecipesWebApp.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IStatisticsService statisticsService;

        public HomeController(
            IRecipeService recipeService,
            IStatisticsService statisticsService)
        {
            this.recipeService = recipeService;
            this.statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            var latestRecipes = recipeService.GetLatestRecipes();

            return View(latestRecipes);
        }

        public IActionResult Statistics()
        {
            var totalStatistics = this.statisticsService.Total();

            return View(totalStatistics);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
