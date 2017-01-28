using System.Web.Mvc;
using MRJ.ServiceLayer.Contracts;
using MRJ.Web.Caching;
using MRJ.Web.DependencyResolution;

namespace MRJ.Web.Filters
{
    public class SiteNameActionFilter : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var siteSettings = IoC.Container.GetInstance<ICacheService>().GetSiteSettings();

            filterContext.Controller.ViewBag.SiteName = siteSettings.SiteName;
            filterContext.Controller.ViewBag.ContactInfo = siteSettings.ContactInfo;
        }

    }
}