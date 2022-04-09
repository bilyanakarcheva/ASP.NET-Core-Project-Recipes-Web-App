namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using RecipesWebApp.Infrastructure;
    using RecipesWebApp.Models.Contributors;
    using System.Linq;

    public class ContributorsController : Controller
    {
        private readonly RecipesDbContext data;

        public ContributorsController(RecipesDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeContributorFormModel contributor)
        {
            var userId = this.User.GetId();

            var userIsAlreadyContributor = this.data
                .Contributors
                .Any(c => c.UserId == userId);

            if (userIsAlreadyContributor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(contributor);
            }

            var contributorData = new Contributor
            {
                UserId = userId,
                FirstName = contributor.FirstName,
                LastName = contributor.LastName
            };

            this.data.Contributors.Add(contributorData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
