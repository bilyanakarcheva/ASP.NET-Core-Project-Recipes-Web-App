namespace RecipesWebApp.Services.MealTypes
{
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Services.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public interface IMealTypesService
    {
        public IEnumerable<RecipeServiceModel> RecipesByMealType(int mealTypeId);

        public IEnumerable<RecipeServiceModel> GetRecipes(IQueryable<Recipe> recipeQuery);
    }
}
