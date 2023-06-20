using FH.App.Extensions.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FH.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class RemoveElementByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set;}

        public RemoveElementByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var hasAccess = CustomAuthorization.ValidateUserClaim(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAccess) return;

            output.SuppressOutput();
        }
    }
}
