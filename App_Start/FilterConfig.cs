using System.Web.Mvc;

namespace DevPath
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // In FilterConfig we can register global filters.

            filters.Add(new HandleErrorAttribute()); // This filter redirects the user to an error page when an action throws an exception.

            filters.Add(new AuthorizeAttribute()); // This filter requires the user to be authenticated to access any controller actions (by default).
        }
    }
}
