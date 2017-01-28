using System.Collections.Generic;
using System.Threading.Tasks;
using MRJ.ViewModels;

namespace MRJ.ServiceLayer.Contracts
{
    public interface ISiteMapService
    {
        Task<IList<SiteMapLinkViewModel>> GetProductsSiteMap();
    }
}
