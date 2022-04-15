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
        {
            this.data = data;
        }

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
                Recipes = recipes,
                TotalRecipes = totalRecipes
            };
        }

        public RecipeDetailsServiceModel Details(int recipeId)
        {
            return this.data
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
                .FirstOrDefault();

        }

        public int Create(
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

            this.data.Recipes.Add(recipeData);
            this.data.SaveChanges();

            return recipeData.Id;
        }


        public bool Edit(
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

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<RecipeServiceModel> MyRecipes(string userId)
        {
            return this.GetRecipes(this.data
                .Recipes
                .Where(r => r.Contributor.UserId == userId));
        }

        public bool RecipeIsByContributor(int recipeId, int contributorId)
        {
            var isContributor = this.data
                .Recipes
                .Any(r => r.Id == recipeId && r.ContributorId == contributorId);

            return isContributor;
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

        public IEnumerable<RecipeMealTypeServiceModel> GetMealTypes()
        {
            return this.data
                .MealTypes
                .Select(t => new RecipeMealTypeServiceModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToList();
        }

        public IEnumerable<RecipeServiceModel> RecipesByMealType(int mealTypeId)
        {
            return this.GetRecipes(this.data
                .Recipes
                .Where(r => r.MealTypeId == mealTypeId));
        }

        public bool MealTypeExists(int mealTypeId)
        {
            return this.data
                .MealTypes
                .Any(m => m.Id == mealTypeId);
        }
    }
}
