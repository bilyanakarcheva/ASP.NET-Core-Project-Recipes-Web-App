namespace RecipesWebApp.Infrastructure
{
    using System.Security.Claims;

    using static WebConstants;

    public static class UserClaimsExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AministratorRoleName);
        }
    }
}
