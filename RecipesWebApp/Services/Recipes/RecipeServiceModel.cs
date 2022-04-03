namespace RecipesWebApp.Services.Recipes
{
    public class RecipeServiceModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public string CookingTime { get; set; }

        public string ImageUrl { get; set; }

        public string MealTypeName { get; init; }

        public int TotalRecipes { get; init; }
    }
}
