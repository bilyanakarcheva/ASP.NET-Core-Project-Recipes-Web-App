namespace RecipesWebApp.Models.Contributors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Contributor;

    public class BecomeContributorFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength =FirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength =LastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
