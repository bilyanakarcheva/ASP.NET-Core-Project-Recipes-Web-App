namespace RecipesWebApp.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeSearchQueryModel
    {
        public const int RecipesPerPage = 2;

        public IEnumerable<string> MealTypes { get; init; }

        [Display(Name = "Search")]
        public string SearchWord { get; init;}

        public RecipeSorting Sorting { get; init; }

        public int CurrentPage { get; set; } = 1;//Check if should be set

        public IEnumerable<RecipeListingViewModel> Recipes { get; init; }
    }
}
