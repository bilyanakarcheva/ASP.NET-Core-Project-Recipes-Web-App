namespace RecipesWebApp.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddRecipeFormModel
    {
        [Required]
        [StringLength(RecipeNameMaxLength, MinimumLength = RecipeNameMinLength, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Display(Name = "Recipe Title")]
        // We use init, since they are not editable in theory. Should stay the way we receive it.
        public string Title { get; init; }

        //Check format later on!!
        [Required]
        [StringLength(CookingTimeMaxLength, MinimumLength = CookingTimeMinLength, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Display(Name = "Cooking Time")]
        public string CookingTime { get; init; }

        [Range(RecipePortionsMinRange, RecipePortionsMaxRange)]
        public int Portions { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = RecipeInstructionsMinLength, ErrorMessage = "{0} must be at least {2} characters.")]
        public string Ingredients { get; init; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = RecipeDescriptionMinLength, ErrorMessage = "{0} must be at least {2} characters.")]
        public string Instructions { get; init; }

        //[Required(ErrorMessage = "{0} is required.")]
        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }


        [Display(Name = "Meal Type")]
        public int MealTypeId { get; init; }

        public IEnumerable<RecipeMealTypeViewModel> MealTypes { get; set; }
    }
}
