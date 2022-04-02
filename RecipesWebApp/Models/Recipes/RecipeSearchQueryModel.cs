namespace RecipesWebApp.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeSearchQueryModel
    {
        public const int RecipesPerPage = 3;

       // public IEnumerable<string> MealTypes { get; init; }

        [Display(Name = "Search")]
        public string SearchWord { get; init;}

        [Display(Name = "Sort")]
        public RecipeSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;//Check if should be set

        public int TotalRecipes { get; set; }

        public IEnumerable<RecipeListingViewModel> Recipes { get; set; }
    }
}
