namespace RecipesWebApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using RecipesWebApp.Models.Contributors;
    using RecipesWebApp.Infrastructure;
    using RecipesWebApp.Services.Contributors;

    public class ContributorsController : Controller
    {
        private readonly IContributorService contributors;

        public ContributorsController(IContributorService contributors)
        {
            this.contributors = contributors;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BecomeContributorFormModel contributor)
        {
            var userId = this.User.GetId();

            var IsContributor = await contributors.UserIsContributor(userId);

            if (IsContributor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(contributor);
            }

            this.contributors.CreateContributor(
                userId,
                contributor.FirstName,
                contributor.LastName);

            return RedirectToAction("Index", "Home");
        }
    }
}
