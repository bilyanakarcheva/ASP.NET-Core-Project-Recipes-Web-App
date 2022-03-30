namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;

    public class RecipesController : Controller
    {
        private readonly RecipesDbContext data;

        public RecipesController(RecipesDbContext data)
         => this.data = data;


        //  HTTP GET - Visualizing the view
        public IActionResult Add() => View(new AddRecipeFormModel
        {
            MealTypes = this.GetRecipeMealTypes()
        });

        // HTTP POST - Recieves data from the form.
        [HttpPost]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
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
                MealTypeId = recipe.MealTypeId
            };

            this.data.Recipes.Add(recipeData);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult All(
            string searchWord,
            RecipeSorting sorting)
        {
            var recipeQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                var searchWordToLower = searchWord.ToLower();

                recipeQuery = recipeQuery.Where(r =>
                r.Title.ToLower().Contains(searchWordToLower) ||
                r.Ingredients.ToLower().Contains(searchWordToLower) ||
                r.Instructions.ToLower().Contains(searchWordToLower));
            }

            recipeQuery = sorting switch
            {
                RecipeSorting.Newest => recipeQuery.OrderByDescending(r => r.Id),
                RecipeSorting.Oldest => recipeQuery.OrderBy(r => r.Id),
                RecipeSorting.TitleAscedning => recipeQuery.OrderBy(r => r.Title),
                RecipeSorting.TitleDescedning => recipeQuery.OrderByDescending(r => r.Title),
                _ => recipeQuery.OrderByDescending(r => r.Id)
                //If enum is non-existent sort by Id
            };

            var recipes = recipeQuery
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    CookingTime = r.CookingTime,
                    ImageUrl = r.ImageUrl,
                    MealType = r.MealType.Name
                })
                .ToList();


            return View(new RecipeSearchViewModel
            {
                Recipes = recipes,
                SearchWord = searchWord,
                Sorting = sorting
            });
        }

        private IEnumerable<RecipeMealTypeViewModel> GetRecipeMealTypes()
            => this.data
                .MealTypes
                .Select(t => new RecipeMealTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToList();


    }
}
