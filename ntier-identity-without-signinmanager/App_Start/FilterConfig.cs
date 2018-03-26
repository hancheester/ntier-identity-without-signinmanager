using System.Web;
using System.Web.Mvc;

namespace ntier_identity_without_signinmanager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
