namespace RecipeWebAppTest
{
    using MyTested.AspNetCore.Mvc;
    using RecipesWebApp.Controllers;
    using Xunit;

    using static Data.Recipes;

    public class HomeControllerTest
    {
        [Fact]
        public void HomeShouldReturnCorrectViewAndModel()
          => MyController<HomeController>
                .Instance(c => c
                    .WithData(TenFakeRecipes));

        
        [Fact]
        public void ErrorShouldReturnView()
        => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View();
    }

}
