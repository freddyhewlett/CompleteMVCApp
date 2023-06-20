using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FH.App.Extensions.Identity
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base (typeof(ClaimFilterRequisite))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
