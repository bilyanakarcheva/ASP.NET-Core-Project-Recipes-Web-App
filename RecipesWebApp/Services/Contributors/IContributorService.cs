namespace RecipesWebApp.Services.Contributors
{
    using System.Threading.Tasks;

    public interface IContributorService
    {
        Task<bool> UserIsContributor(string userId);

        Task<int> GetContributorId(string userId);

        void CreateContributor(string userId, string firstName, string lastName);

    }
}
