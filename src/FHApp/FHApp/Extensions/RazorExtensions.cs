using Microsoft.AspNetCore.Mvc.Razor;

namespace FH.App.Extensions
{
    public static class RazorExtensions
    {
        public static string DocumentFormat(this RazorPage page, int devType, string document)
        {
            return devType == 1 ? Convert.ToInt64(document).ToString(@"000\.000\.000\-00") : Convert.ToInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
