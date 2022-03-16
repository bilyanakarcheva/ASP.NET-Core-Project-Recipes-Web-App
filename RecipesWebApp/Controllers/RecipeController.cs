namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;


    public class RecipeController : Controller
    {
        //  HTTP GET 
        public IActionResult Add() => View();

        //[HttpPost]
       // public IActionResult Add() => View();
    }
}
