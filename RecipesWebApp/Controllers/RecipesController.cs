namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using RecipesWebApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using RecipesWebApp.Infrastructure;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipes;
        private readonly RecipesDbContext data;

        public RecipesController(IRecipeService recipes, RecipesDbContext data)
        {
            this.recipes = recipes;
            this.data = data;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsContributor())
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            return View(new AddRecipeFormModel
            {
                MealTypes = this.GetRecipeMealTypes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            var contributorId = this.data
                .Contributors
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (!this.UserIsContributor())
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            if (!this.data.MealTypes.Any(m => m.Id == recipe.MealTypeId))
            {
                this.ModelState.AddModelError(nameof(recipe.MealTypeId), "Meal Type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                recipe.MealTypes = this.GetRecipeMealTypes();

                return View(recipe);
            }

            var recipeData = new Recipe
            {
                Title = recipe.Title,
                CookingTime = recipe.CookingTime,
                Portions = recipe.Portions,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                MealTypeId = recipe.MealTypeId,
                ContributorId = contributorId
            };

            this.data.Recipes.Add(recipeData);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult All([FromQuery]RecipeSearchQueryModel query)
        {
            var queryResult = this.recipes.All(
                query.SearchWord,
                query.Sorting,
                query.CurrentPage,
                RecipeSearchQueryModel.RecipesPerPage);

            query.TotalRecipes = queryResult.TotalRecipes;
            query.Recipes = queryResult.Recipes;

            return View(query);
        }

        private IEnumerable<RecipeMealTypeViewModel> GetRecipeMealTypes()
        {
          return this.data
                  .MealTypes
                  .Select(t => new RecipeMealTypeViewModel
                  {
                      Id = t.Id,
                      Name = t.Name
                  })
                  .ToList();

        }

        private bool UserIsContributor()
        {
            var userIsContributor = this.data
                .Contributors
                .Any(c => c.UserId == this.User.GetId());

            return userIsContributor;
        }
    }
}
