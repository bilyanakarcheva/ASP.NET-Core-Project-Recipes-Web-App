namespace RecipesWebApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Recipe
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(RecipeNameMaxLength)]
        public string Name { get; set; }

        //Check format later on!!
        [Required]
        [MaxLength(RecipeCookingTimeMaxLength)]
        public string CookingTime { get; set; }

        [Required]
        [Range(RecipePortionsMinRange, RecipePortionsMaxRange)]
        public int Portions { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int MealTypeId { get; set; }

        //Init - not editable
        public MealType MealType { get; init; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
    }

}
