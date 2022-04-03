namespace RecipesWebApp.Services
{
    using RecipesWebApp.Models.Recipes;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(
            string searchWord,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);
    }
}
