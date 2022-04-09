namespace RecipesWebApp.Infrastructure
{
    using RecipesWebApp.Data;
    using System.Linq;
    using System.Security.Claims;

    public static class UserClaimsExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
