using System.Web.Mvc;
using MRJ.Web.Filters;

namespace MRJ.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonHandlerAttribute());
            filters.Add(new SiteNameActionFilter());
        }
    }
}
