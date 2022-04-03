namespace RecipesWebApp.Services
{
    using RecipesWebApp.Services.Recipes;
    using System.Collections.Generic;

    public class RecipeQueryServiceModel
    {
        public int CurrentPage { get; init; }
        public int RecipesPerPage { get; init; }

        public int TotalRecipes { get; set; }

        public IEnumerable<RecipeServiceModel> Recipes { get; init; }
    }
}
