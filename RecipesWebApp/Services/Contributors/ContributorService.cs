namespace RecipesWebApp.Services.Contributors
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq;
    using RecipesWebApp.Data;
    using RecipesWebApp.Data.Models;

    public class ContributorService : IContributorService
    {
        private readonly RecipesDbContext data;

        public ContributorService(RecipesDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> UserIsContributor(string userId){
            return await this.data
                    .Contributors
                    .AnyAsync(c => c.UserId == userId);
        }

        public async Task<int> GetContributorId(string userId)
        {
            return await this.data
                .Contributors
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
        }

        public async void CreateContributor(
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

            await this.data.Contributors.AddAsync(contributorData);
            await this.data.SaveChangesAsync();

        }
    }
}
