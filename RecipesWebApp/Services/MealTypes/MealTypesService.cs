namespace RecipesWebApp.Services.MealTypes
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Services.Recipes;

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

        public async Task<int> GetMealTypeId(string mealTypeName)
        {
             var mealType = await this.data
                .MealTypes
                .Where(m => m.Name == mealTypeName)
                .FirstOrDefaultAsync();

            var mealtypeId = mealType.Id;

            return mealtypeId;
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
