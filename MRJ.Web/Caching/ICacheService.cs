using MRJ.ViewModels;

namespace MRJ.Web.Caching
{
    public interface ICacheService
    {
        EditSettingViewModel GetSiteSettings();
        void RemoveSiteSettings();
    }
}