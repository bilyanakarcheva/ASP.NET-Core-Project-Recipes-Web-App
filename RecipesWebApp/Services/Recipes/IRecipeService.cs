namespace RecipesWebApp.Services
{
    using RecipesWebApp.Models.Recipes;
    using RecipesWebApp.Services.Recipes;
    using System.Collections.Generic;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(
            string searchWord,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        IEnumerable<RecipeServiceModel> MyRecipes(string userId);
    }
}
