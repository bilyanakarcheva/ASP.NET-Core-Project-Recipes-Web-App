﻿namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RecipesWebApp.Data;
    using RecipesWebApp.Models.Recipes;
    using RecipesWebApp.Infrastructure;
    using RecipesWebApp.Services.Contributors;
    using RecipesWebApp.Services.Recipes;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipes;
        private readonly IContributorService contributors;
        private readonly RecipesDbContext data;

        public RecipesController(IRecipeService recipes, 
            RecipesDbContext data, 
            IContributorService contributors)
        {
            this.recipes = recipes;
            this.data = data;
            this.contributors = contributors;
        }

        [Authorize]
        public IActionResult MyRecipes()
        {
            var myRecipes = this.recipes.MyRecipes(this.User.GetId());

            return View(myRecipes);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.contributors.UserIsContributor(this.User.GetId()))
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            return View(new RecipeFormModel
            {
                MealTypes = this.recipes.GetMealTypes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(RecipeFormModel recipe)
        {
            var contributorId = this.contributors.GetContributorId(this.User.GetId());

            if (!this.contributors.UserIsContributor(this.User.GetId()))
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            if (!this.recipes.MealTypeExists(recipe.MealTypeId))
            {
                this.ModelState.AddModelError(nameof(recipe.MealTypeId), "Meal Type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                recipe.MealTypes = this.recipes.GetMealTypes();

                return View(recipe);
            }

            this.recipes.Create(
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

        [Authorize]
        public IActionResult Edit(int recipeId)
        {
            var userId = this.User.GetId();

            if (!this.contributors.UserIsContributor(userId))
            {
                return RedirectToAction(nameof(ContributorsController.Create), "Contributors");
            }

            var recipe = this.recipes.Details(recipeId);

            if (recipe.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new RecipeFormModel
            {
                Title = recipe.Title,
                CookingTime = recipe.CookingTime,
                Portions = recipe.Portions,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                MealTypeId = recipe.MealTypeId,
                MealTypes = this.recipes.GetMealTypes()
            });
        }

    }
}
