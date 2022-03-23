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

            return RedirectToAction("Index", "Home");
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
