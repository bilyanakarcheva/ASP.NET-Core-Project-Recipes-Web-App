namespace RecipesWebApp.Services.Recipes
{
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipeService : IRecipeService
    {
        private readonly RecipesDbContext data;

        public RecipeService(RecipesDbContext data)
            => this.data = data;

        public RecipeQueryServiceModel All(
            string searchWord,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage
            )
        {
            var recipeQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                var searchWordToLower = searchWord.ToLower();

                recipeQuery = recipeQuery.Where(r =>
                r.Title.ToLower().Contains(searchWordToLower) ||
                r.Ingredients.ToLower().Contains(searchWordToLower) ||
                r.Instructions.ToLower().Contains(searchWordToLower));
            }

            recipeQuery = sorting switch
            {
                RecipeSorting.Newest => recipeQuery.OrderByDescending(r => r.Id),
                RecipeSorting.Oldest => recipeQuery.OrderBy(r => r.Id),
                RecipeSorting.TitleAscedning => recipeQuery.OrderBy(r => r.Title),
                RecipeSorting.TitleDescedning => recipeQuery.OrderByDescending(r => r.Title),
                _ => recipeQuery.OrderByDescending(r => r.Id)
                //If enum is non-existent sort by Id
            };

            var totalRecipes = recipeQuery.Count();

            var recipes = GetRecipes(recipeQuery
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage));

            return new RecipeQueryServiceModel
            {
                CurrentPage = currentPage,
                RecipesPerPage = recipesPerPage,
                Recipes = recipes
            };
        }

        public IEnumerable<RecipeServiceModel> MyRecipes(string userId)
        {
            return this.GetRecipes(this.data
                .Recipes
                .Where(r => r.Contributor.UserId == userId));
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
