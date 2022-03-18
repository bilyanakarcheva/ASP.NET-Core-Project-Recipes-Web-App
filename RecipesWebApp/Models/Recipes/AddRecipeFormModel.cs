namespace RecipesWebApp.Models.Recipes
{
    public class AddRecipeFormModel
    {
        // We use init, since they are not editable in theory. Should stay the way we receive it.
        public string Title { get; init; }

        //Check format later on!!
        public string CookingTime { get; init; }

        public int Portions { get; init; }

        public string Instructions { get; init; }

        public string ImageUrl { get; init; }

        public int MealTypeId { get; init; }
    }
}
