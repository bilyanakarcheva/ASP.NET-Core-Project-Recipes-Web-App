namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Models.Recipes;

    public class RecipesController : Controller
    {
        //  HTTP GET - Visualizing the view
        public IActionResult Add() => View();

        [HttpPost]
       public IActionResult Add(AddRecipeFormModel recipe)
        {
            return View();
        }
    }
}
