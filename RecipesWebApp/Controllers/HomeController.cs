namespace RecipesWebApp.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Services.Recipes;
    using RecipesWebApp.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly RecipesDbContext data;

        public HomeController(
            IStatisticsService statistics,
            RecipesDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalStatistics = this.statistics.Total();
            var totalRecipes = totalStatistics.TotalRecipes;

            var recipes = this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeServiceModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    CookingTime = r.CookingTime,
                    ImageUrl = r.ImageUrl,
                    MealTypeName = r.MealType.Name
                })
                .Take(6)
                .ToList();


            return View(recipes);
        }

        public IActionResult Statistics()
        {
            var totalStatistics = this.statistics.Total();
            var totalRecipes = totalStatistics.TotalRecipes;

            return View(totalStatistics);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
