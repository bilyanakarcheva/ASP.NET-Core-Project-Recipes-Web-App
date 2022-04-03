namespace RecipesWebApp.Models.Api
{
    using RecipesWebApp.Models.Recipes;

    public class AllRecipesApiRequestModel
    {

        public string SearchTerm { get; init; }

        public RecipeSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int RecipesPerPage { get; init; } = 10;
    }
}
