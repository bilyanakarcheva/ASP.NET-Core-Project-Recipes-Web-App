namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Services.MealTypes;

    public class MealTypesController : Controller
    {
        private readonly MealTypesService recipes;
        private readonly RecipesDbContext data;


        public MealTypesController(MealTypesService recipes)
        {
            this.recipes = recipes;  
        }

        public IActionResult AllSoups()
        {
            int mealTypeId = 1;
            var soups = this.recipes.RecipesByMealType(mealTypeId);

            return View(soups);
        }
        public IActionResult AllSalads()
        {
            return View();
        }
        public IActionResult AllMainCourses()
        {
            return View();
        }
        public IActionResult AllDesserts()
        {
            return View();
        }
    }
}
