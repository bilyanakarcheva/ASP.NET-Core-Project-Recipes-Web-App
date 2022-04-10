namespace RecipesWebApp.Services.Contributors
{
    using RecipesWebApp.Data;
    using System.Linq;

    public class ContributorService : IContributorService
    {
        private readonly RecipesDbContext data;

        public ContributorService(RecipesDbContext data)
        {
            this.data = data;
        }

        public bool IsContributor(string userId)
        {
            return this.data
                    .Contributors
                    .Any(c => c.UserId == userId);
        }
    }
}
