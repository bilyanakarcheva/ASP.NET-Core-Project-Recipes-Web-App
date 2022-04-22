namespace RecipeWebAppTest
{
    using System;
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using RecipesWebApp.Controllers;
    using RecipesWebApp.Services.Recipes;
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
