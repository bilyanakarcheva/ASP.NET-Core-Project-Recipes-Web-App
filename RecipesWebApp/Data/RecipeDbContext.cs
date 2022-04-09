namespace RecipesWebApp.Data
{
    using Microsoft.AspNetCore.Identity;
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

        // DbSet<Ingredient> Ingredients { get; init; }

        public DbSet<MealType> MealTypes { get; init; }

        public DbSet<Contributor> Contributors { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Recipe>()
                .HasOne(r => r.MealType)
                .WithMany(r => r.Recipes)
                .HasForeignKey(r => r.MealTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Recipe>()
                .HasOne(r => r.Contributor)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.ContributorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Contributor>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Contributor>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
