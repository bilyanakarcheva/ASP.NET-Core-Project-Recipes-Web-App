namespace RecipesWebApp.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RecipesWebApp.Models.Recipes;
    using RecipesWebApp.Infrastructure;
    using RecipesWebApp.Services.Contributors;
    using RecipesWebApp.Services.Recipes;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipes;
        private readonly IContributorService contributors;


        public RecipesController(IRecipeService recipes,  
            IContributorService contributors)
        {
            this.recipes = recipes;
            this.contributors = contributors;
        }

        [Authorize]
        public async Task<IActionResult> MyRecipes()
        {
            var myRecipes = await this.recipes.MyRecipes(this.User.GetId());

            return View(myRecipes);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            var recipe = await this.recipes.Details(id);

            return View(new RecipeFormModel
            {
                Title = recipe.Title,
                CookingTime = recipe.CookingTime,
                Portions = recipe.Portions,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                MealTypeId = recipe.MealTypeId,
            });
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            var isContributor = await this.contributors.UserIsContributor(this.User.GetId());

            if (!isContributor && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            return View(new RecipeFormModel
            {
                MealTypes = await this.recipes.GetMealTypes()
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(RecipeFormModel recipe)
        {
            var contributorId = await this.contributors.GetContributorId(this.User.GetId());

            var isContributor = await this.contributors.UserIsContributor(this.User.GetId());

            if (!isContributor && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            var mealTypeExists = await this.recipes.MealTypeExists(recipe.MealTypeId);

            if (!mealTypeExists)
            {
                this.ModelState.AddModelError(nameof(recipe.MealTypeId), "Meal Type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                recipe.MealTypes = await this.recipes.GetMealTypes();

                return View(recipe);
            }

            await this.recipes.Create(
                recipe.Title,
                recipe.CookingTime,
                recipe.Portions,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.ImageUrl,
                recipe.MealTypeId,
                contributorId);

            return RedirectToAction(nameof(All));
        }

        //CHECK
        public async Task<IActionResult> All([FromQuery]RecipeSearchQueryModel query)
        {
            var queryResult = await this.recipes.All(
                query.SearchWord,
                query.Sorting,
                query.CurrentPage,
                RecipeSearchQueryModel.RecipesPerPage);

            query.TotalRecipes = queryResult.TotalRecipes;
            query.Recipes = queryResult.Recipes;

            return View(query);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetId();

            var isContributor = await this.contributors.UserIsContributor(this.User.GetId());

            if (!isContributor && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            var recipe = await this.recipes.Details(id);

            if (recipe.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var mealTypes = await this.recipes.GetMealTypes();

            return View(new RecipeFormModel
            {
                Title = recipe.Title,
                CookingTime = recipe.CookingTime,
                Portions = recipe.Portions,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                MealTypeId = recipe.MealTypeId,
                MealTypes = mealTypes
            }) ; 
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecipeFormModel recipe)
        {
            var contributorId = await this.contributors.GetContributorId(this.User.GetId());

            if (contributorId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            var mealTypeExists = await this.recipes.MealTypeExists(recipe.MealTypeId);

            if (!mealTypeExists)
            {
                this.ModelState.AddModelError(nameof(recipe.MealTypeId), "Meal Type does not exist.");
            }

            var mealTypes = await this.recipes.GetMealTypes();

            if (!ModelState.IsValid)
            {
                recipe.MealTypes = mealTypes;

                return View(recipe);
            }

            var isByContributor = await this.recipes.RecipeIsByContributor(id, contributorId);

            if (!isByContributor && !User.IsAdmin())
            {
                return BadRequest();
            }

            await this.recipes.Edit(
                id,
                recipe.Title,
                recipe.CookingTime,
                recipe.Portions,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.ImageUrl,
                recipe.MealTypeId);

            return RedirectToAction(nameof(MyRecipes));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.recipes.Delete(id);

            return RedirectToAction(nameof(MyRecipes));
        }

    }
}
