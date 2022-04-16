namespace RecipesWebApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Recipe;

    public class Recipe
    {
        public int Id { get; init; }

        //Text field
        [Required]
        [MaxLength(NameMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(CookingTimeMaxLength)]
        public string CookingTime { get; set; }

        //Dropdown - 1 to 20
        [Required]
        [Range(PortionsMinRange, PortionsMaxRange)]
        public int Portions { get; set; }

        [Required]
        public string Ingredients { get; set; }

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

        public int ContributorId { get; init; }

        public Contributor Contributor { get; init; }

    }

}
