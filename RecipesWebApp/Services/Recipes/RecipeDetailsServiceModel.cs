namespace RecipesWebApp.Services.Recipes
{
    public class RecipeDetailsServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int Portions  { get; init; }

        public string CookingTime { get; init; }

        public string Ingredients { get; init; }

        public string Instructions { get; init; }

        public string ImageUrl { get; init; }

        public int MealTypeId { get; init; }

        public string MealTypeName { get; init; }

        public int ContributorId { get; init; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserId { get; init; }

    }
}
