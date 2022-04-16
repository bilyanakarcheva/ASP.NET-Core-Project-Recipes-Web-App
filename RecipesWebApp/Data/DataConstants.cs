namespace RecipesWebApp.Data
{
    public class DataConstants
    {
        public class Recipe
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
            public const int PortionsMinRange = 1;
            public const int PortionsMaxRange = 20;
            public const int DescriptionMinLength = 10;
            public const int InstructionsMinLength = 5;
            public const int CookingTimeMinLength = 2;
            public const int CookingTimeMaxLength = 20;
        }

        public class MealType
        {
            public const int NameMaxLength = 30;
        }

        public class Contributor
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 20;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;

        }

        public class User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }
    }
}
