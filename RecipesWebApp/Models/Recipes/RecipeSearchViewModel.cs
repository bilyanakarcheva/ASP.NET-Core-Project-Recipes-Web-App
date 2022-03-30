namespace RecipesWebApp.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RecipeSearchViewModel
    {
        public IEnumerable<string> MealTypes { get; init; }

        [Display(Name = "Search")]
        public string SearchWord { get; init;}

        public RecipeSorting Sorting { get; init; }
 
        public IEnumerable<RecipeListingViewModel> Recipes { get; init; }
    }
}
