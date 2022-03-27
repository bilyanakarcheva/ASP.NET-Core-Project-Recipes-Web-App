namespace RecipesWebApp.Models.Recipes
{
    public class RecipeListingViewModel
    {
        //Model to visialize some of the details of the Recipes

        public int Id { get; init; }

        public string Title { get; init; }

        public string CookingTime { get; init; }

        public string ImageUrl { get; init; }

        public string MealType { get; init; }
    }
}
