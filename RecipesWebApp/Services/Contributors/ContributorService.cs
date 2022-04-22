namespace RecipesWebApp.Services.Contributors
{
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;
    using System.Linq;

    public class ContributorService : IContributorService
    {
        private readonly RecipesDbContext data;

        public ContributorService(RecipesDbContext data)
        {
            this.data = data;
        }

        public bool UserIsContributor(string userId){
            return this.data
                    .Contributors
                    .Any(c => c.UserId == userId);
        }

        public int GetContributorId(string userId)
        {
            return this.data
                .Contributors
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefault();
        }

        public void CreateContributor(
            string userId,
            string firstName,
            string lastName)
        {
            var contributorData = new Contributor
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName
            };

            this.data.Contributors.Add(contributorData);
            this.data.SaveChanges();

        }

        public void CreateContributor(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
