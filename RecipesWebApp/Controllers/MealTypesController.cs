namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using RecipesWebApp.Services.MealTypes;

    public class MealTypesController : Controller
    {
        private readonly IMealTypesService mealTypes;


        public MealTypesController(IMealTypesService mealTypes)
        {
            this.mealTypes = mealTypes;  
        }

        public async Task<IActionResult> Soups()
        {
            int mealTypeId = await this.mealTypes.GetMealTypeId(nameof(Soups));
            var soups = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(soups);
        }
        public async Task<IActionResult> Salads()
        {
            int mealTypeId = await this.mealTypes.GetMealTypeId(nameof(Salads));
            var salads = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(salads);
        }
        public async Task<IActionResult> MainCourses()
        {
            int mealTypeId = await this.mealTypes.GetMealTypeId("Main Courses");
            var mainCourse = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(mainCourse);
        }
        public async Task<IActionResult> Desserts()
        {
            int mealTypeId = await this.mealTypes.GetMealTypeId(nameof(Desserts));
            var dessert = this.mealTypes.RecipesByMealType(mealTypeId);

            return View(dessert);
        }
    }
}
