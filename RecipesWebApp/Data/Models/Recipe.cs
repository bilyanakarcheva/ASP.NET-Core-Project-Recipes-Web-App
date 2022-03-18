namespace RecipesWebApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Recipe
    {
        public int Id { get; init; }

        //Text field
        [Required]
        [MaxLength(RecipeNameMaxLength)]
        public string Title { get; set; }

        //Check format later on!! Drop down - hours and minites
        [Required]
        [MaxLength(RecipeCookingTimeMaxLength)]
        public string CookingTime { get; set; }

        //Dropdown - 1 to 10
        [Required]
        [Range(RecipePortionsMinRange, RecipePortionsMaxRange)]
        public int Portions { get; set; }

        //Text field
        [Required]
        public string Instructions { get; set; }

        //Text field
        [Required]
        public string ImageUrl { get; set; }

        //Drop down - one choice
        public int MealTypeId { get; set; }

        //Init - not editable
        public MealType MealType { get; init; }

        //How to add them?
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }

}
