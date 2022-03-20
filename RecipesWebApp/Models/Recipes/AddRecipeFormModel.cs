namespace RecipesWebApp.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddRecipeFormModel
    {
        [Required]
        [MaxLength(RecipeNameMaxLength)]
        [MinLength(RecipeNameMinLength)]
        [Display(Name = "Recipe Title")]
        // We use init, since they are not editable in theory. Should stay the way we receive it.
        public string Title { get; init; }

        //Check format later on!!
        public string CookingTime { get; init; }

        public int Portions { get; init; }

        public string Ingredients { get; init; }

        public string Instructions { get; init; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Meal Type")]
        public int MealTypeId { get; init; }

        public IEnumerable<RecipeMealTypeViewModel> MealTypes { get; set; }
    }
}
