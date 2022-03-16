namespace RecipesWebApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RecipesWebApp.Data.Models;

    public class RecipesDbContext : IdentityDbContext
    {
        public RecipesDbContext(DbContextOptions<RecipesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<Ingredient> Ingredients { get; init; }

        public DbSet<MealType> MealTypes { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Recipe>()
                .HasOne(r => r.MealType)
                .WithMany(r => r.Recipes)
                .HasForeignKey(r => r.MealTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
