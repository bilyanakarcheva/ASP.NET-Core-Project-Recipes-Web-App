namespace RecipesWebApp.Services.Recipes
{
    using RecipesWebApp.Models.Recipes;
    using System.Collections.Generic;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(
            string searchWord,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        RecipeDetailsServiceModel Details(int recipeId);

        int Create(
                string title,
                string cookingTime,
                int portions,
                string ingredients,
                string instructions,
                string imageUrl,
                int mealTypeId,
                int contributorId);

        IEnumerable<RecipeServiceModel> MyRecipes(string userId);

        IEnumerable<RecipeMealTypeServiceModel> GetMealTypes();

        bool MealTypeExists(int mealTypeId);
    }
}
