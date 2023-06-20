namespace FH.App.Extensions.Identity
{
    public class CustomAuthorization
    {
        public static bool ValidateUserClaim(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }
}
