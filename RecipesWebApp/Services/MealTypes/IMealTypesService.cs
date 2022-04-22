namespace RecipesWebApp.Services.MealTypes
{
    using System.Linq;
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Services.Recipes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMealTypesService
    {
        IEnumerable<RecipeServiceModel> RecipesByMealType(int mealTypeId);

        Task<int> GetMealTypeId(string mealTypeName);

        IEnumerable<RecipeServiceModel> GetRecipes(IQueryable<Recipe> recipeQuery);
    }
}
