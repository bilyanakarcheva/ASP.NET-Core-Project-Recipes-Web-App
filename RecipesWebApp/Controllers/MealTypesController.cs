namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Services.MealTypes;
    using RecipesWebApp.Services.Recipes;

    public class MealTypesController : Controller
    {
        private readonly IMealTypesService mealTypes;
        private readonly IRecipeService recipes;


        public MealTypesController(IMealTypesService mealTypes)
        {
            this.mealTypes = mealTypes;  
        }

        public IActionResult Soups()
        {
            int mealTypeId = this.mealTypes.GetMealTypeId(nameof(Soups));
            var soups = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(soups);
        }
        public IActionResult Salads()
        {
            int mealTypeId = this.mealTypes.GetMealTypeId(nameof(Salads));
            var salads = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(salads);
        }
        public IActionResult MainCourses()
        {
            int mealTypeId = this.mealTypes.GetMealTypeId("Main Courses");
            var mainCourse = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(mainCourse);
        }
        public IActionResult Desserts()
        {
            int mealTypeId = this.mealTypes.GetMealTypeId(nameof(Desserts));
            var dessert = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(dessert);
        }
    }
}
