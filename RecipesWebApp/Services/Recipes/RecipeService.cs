namespace RecipesWebApp.Services.Recipes
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Models.Recipes;

    public class RecipeService : IRecipeService
    {
        private readonly RecipesDbContext data;

        public RecipeService(RecipesDbContext data)
        {
            this.data = data;
        }

        public async Task<RecipeQueryServiceModel> All(
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

            var recipes = await GetRecipes(recipeQuery
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage));

            return new RecipeQueryServiceModel
            {
                CurrentPage = currentPage,
                RecipesPerPage = recipesPerPage,
                Recipes = recipes,
                TotalRecipes = totalRecipes
            };
        }

        public async Task<List<RecipeServiceModel>> GetLatestRecipes()
        {
            var recipes = await this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeServiceModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    CookingTime = r.CookingTime,
                    ImageUrl = r.ImageUrl,
                    MealTypeName = r.MealType.Name
                })
                .Take(6)
                .ToListAsync();

            return recipes;
        }

        public async Task<RecipeDetailsServiceModel> Details(int recipeId)
        {
            return await this.data
                .Recipes
                .Where(r => r.Id == recipeId)
                .Select(r => new RecipeDetailsServiceModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    CookingTime = r.CookingTime,
                    Ingredients = r.Ingredients,
                    Instructions = r.Instructions,
                    ImageUrl = r.ImageUrl,
                    Portions = r.Portions,
                    MealTypeName = r.MealType.Name,
                    ContributorId = r.ContributorId,
                    FirstName = r.Contributor.FirstName,
                    LastName = r.Contributor.LastName,
                    UserId = r.Contributor.UserId
                })
                .FirstOrDefaultAsync();

        }

        public async Task<int> Create(
            string title,
            string cookingTime,
            int portions,
            string ingredients,
            string instructions,
            string imageUrl,
            int mealTypeId,
            int contributorId)
        {
            var recipeData = new Recipe
            {
                Title = title,
                CookingTime = cookingTime,
                Portions = portions,
                Ingredients = ingredients,
                Instructions = instructions,
                ImageUrl = imageUrl,
                MealTypeId = mealTypeId,
                ContributorId = contributorId
            };

            await this.data.Recipes.AddAsync(recipeData);
            await this.data.SaveChangesAsync();

            return recipeData.Id;
        }


        public async Task<bool> Edit(
            int recipeId,
            string title,
            string cookingTime,
            int portions,
            string ingredients,
            string instructions,
            string imageUrl,
            int mealTypeId)
        {
            var recipeData = this.data.Recipes.Find(recipeId);

            if (recipeData == null)
            {
                return false;
            }

            recipeData.Title = title;
            recipeData.CookingTime = cookingTime;
            recipeData.Portions = portions;
            recipeData.Ingredients = ingredients;
            recipeData.Instructions = instructions;
            recipeData.ImageUrl = imageUrl;
            recipeData.MealTypeId = mealTypeId;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int recipeId)
        {
            var recipeData = await this.data.Recipes.FindAsync(recipeId);

            if (recipeData == null)
            {
                return false;
            }

            this.data.Recipes.Remove(recipeData);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RecipeServiceModel>> MyRecipes(string userId)
        {
            return await this.GetRecipes(this.data
                .Recipes
                .Where(r => r.Contributor.UserId == userId));
        }

        public async Task<bool> RecipeIsByContributor(int recipeId, int contributorId)
        {
            var isContributor = await this.data
                .Recipes
                .AnyAsync(r => r.Id == recipeId && r.ContributorId == contributorId);

            return isContributor;
        }

        public async Task<IEnumerable<RecipeServiceModel>> GetRecipes(IQueryable<Recipe> recipeQuery)
        {
            return await recipeQuery
                    .Select(r => new RecipeServiceModel
                    {
                        Id = r.Id,
                        Title = r.Title,
                        CookingTime = r.CookingTime,
                        ImageUrl = r.ImageUrl,
                        MealTypeName = r.MealType.Name
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<RecipeMealTypeServiceModel>> GetMealTypes()
        {
            return await this.data
                .MealTypes
                .Select(t => new RecipeMealTypeServiceModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RecipeServiceModel>> RecipesByMealType(int mealTypeId)
        {
            return await this.GetRecipes(this.data
                .Recipes
                .Where(r => r.MealTypeId == mealTypeId));
        }

        public async Task<bool> MealTypeExists(int mealTypeId)
        {
            return await this.data
                .MealTypes
                .AnyAsync(m => m.Id == mealTypeId);
        }
    }
}
