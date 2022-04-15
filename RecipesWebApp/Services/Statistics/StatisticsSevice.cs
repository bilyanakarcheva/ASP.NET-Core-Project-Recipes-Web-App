namespace RecipesWebApp.Services.Statistics
{
    using RecipesWebApp.Data;
    using System.Linq;

    public class StatisticsSevice : IStatisticsService
    {
        private readonly RecipesDbContext data;

        public StatisticsSevice(RecipesDbContext data)
            => this.data = data;


        public StatisticsServiceModel Total()
        {
            var totalRecipes = this.data.Recipes.Count();
            var totalUsers = this.data.Users.Count();
            var totalContributors = this.data.Contributors.Count();

            return new StatisticsServiceModel
            {
                TotalRecipes = totalRecipes,
                TotalUsers = totalUsers,
                TotalContributors = totalContributors
            };
        }
    }
}
