namespace RecipesWebApp.Services.MealTypes
{
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Services.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public class MealTypesService : IMealTypesService
    {
        private readonly RecipesDbContext data;

        public MealTypesService(RecipesDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<RecipeServiceModel> RecipesByMealType(int mealTypeId)
        {
            return this.GetRecipes(this.data
                .Recipes
                .Where(r => r.MealTypeId == mealTypeId));
        }

        public IEnumerable<RecipeServiceModel> GetRecipes(IQueryable<Recipe> recipeQuery)
        {

            return recipeQuery
                    .Select(r => new RecipeServiceModel
                    {
                        Id = r.Id,
                        Title = r.Title,
                        CookingTime = r.CookingTime,
                        ImageUrl = r.ImageUrl,
                        MealTypeName = r.MealType.Name
                    })
                    .ToList();
        }
    }
}
