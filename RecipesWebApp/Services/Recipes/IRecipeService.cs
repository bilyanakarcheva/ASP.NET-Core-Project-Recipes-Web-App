namespace RecipesWebApp.Services.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesWebApp.Models.Recipes;

    public interface IRecipeService
    {
        Task<RecipeQueryServiceModel> All(
            string searchWord,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        Task<List<RecipeServiceModel>> GetLatestRecipes();

        Task<RecipeDetailsServiceModel> Details(int recipeId);

        Task<int> Create(
                string title,
                string cookingTime,
                int portions,
                string ingredients,
                string instructions,
                string imageUrl,
                int mealTypeId,
                int contributorId);


        Task<bool> Edit(
            int id,
            string title,
            string cookingTime,
            int portions,
            string ingredients,
            string instructions,
            string imageUrl,
            int mealTypeId);

        Task<bool> Delete(int id);

        Task<IEnumerable<RecipeServiceModel>> MyRecipes(string userId);

        Task<IEnumerable<RecipeServiceModel>> RecipesByMealType(int mealTypeId);


        Task<IEnumerable<RecipeMealTypeServiceModel>> GetMealTypes();

        Task<bool> MealTypeExists(int mealTypeId);
        Task<bool> RecipeIsByContributor(int recipeId, int contributorId);
    }
}
