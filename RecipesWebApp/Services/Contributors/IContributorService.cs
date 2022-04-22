namespace RecipesWebApp.Services.Contributors
{
    public interface IContributorService
    {
        public bool UserIsContributor(string userId);

        public int GetContributorId(string userId);

        public void CreateContributor(string userId, string firstName, string lastName);
    }
}
