using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipesWebApp.Data.Models
{
    public class Recipe
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.RecipeNameMaxLength)]
        public string Name { get; set; }

        public string CookingTime { get; set; }

        public int Portions { get; set; }

        public string Details { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }

}
