namespace RecipeWebAppTest.Data
{

    using System.Collections.Generic;
    using System.Linq;
    using RecipesWebApp.Data.Models;

    public static class Recipes
    {
        public static IEnumerable<Recipe> TenFakeRecipes
            => Enumerable.Range(0, 10).Select(i => new Recipe());
    }
}
