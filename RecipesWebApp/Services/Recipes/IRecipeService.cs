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

        public List<RecipeServiceModel> GetLatestRecipes();

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

        bool Edit(
            int id,
            string title,
            string cookingTime,
            int portions,
            string ingredients,
            string instructions,
            string imageUrl,
            int mealTypeId);

        bool Delete(int id);

        IEnumerable<RecipeServiceModel> MyRecipes(string userId);

        IEnumerable<RecipeServiceModel> RecipesByMealType(int mealTypeId);


        IEnumerable<RecipeMealTypeServiceModel> GetMealTypes();

        bool MealTypeExists(int mealTypeId);
        bool RecipeIsByContributor(int recipeId, int contributorId);
    }
}
