namespace RecipesWebApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Ingredient
    {
        public int Id { get; init; }

        [Required]
        public string Name { get;}

        [Required]
        [MaxLength(IngredientQuantityMaxLength)]
        public string Quantity { get; set; }

        public int RecipeId { get; set; }

        //Init - not editable
        public Recipe Recipe { get; init; }


    }
}