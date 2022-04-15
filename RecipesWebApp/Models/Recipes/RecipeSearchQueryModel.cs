namespace RecipesWebApp.Models.Recipes
{
    using RecipesWebApp.Services.Recipes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeSearchQueryModel
    {
        public const int RecipesPerPage = 3;

        [Display(Name = "Search")]
        public string SearchWord { get; init;}

        [Display(Name = "Sort")]
        public RecipeSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalRecipes { get; set; }

        public IEnumerable<RecipeServiceModel> Recipes { get; set; }
    }
}
