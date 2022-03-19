namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipesController : Controller
    {
        private readonly RecipesDbContext data;

        public RecipesController(RecipesDbContext data)
        {
            this.data = data;
        }

        //  HTTP GET - Visualizing the view
        public IActionResult Add() => View(new AddRecipeFormModel
        {
            MealTypes = this.GetRecipeMealTypes()
        });

        [HttpPost]
       public IActionResult Add(AddRecipeFormModel recipe)
        {
            return View();
        }

        private IEnumerable<RecipeMealTypeViewModel> GetRecipeMealTypes()
            => this.data
                .MealTypes
                .Select(t => new RecipeMealTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToList();

        
    }
}
