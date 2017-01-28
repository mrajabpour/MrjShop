using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EFSecondLevelCache;
using MRJ.DataLayer;
using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using MRJ.ViewModels;

namespace MRJ.ServiceLayer
{
    public class SiteMapService : ISiteMapService
    {
        private readonly IDbSet<Product> _products;

        public SiteMapService(IUnitOfWork unitOfWork)
        {
            _products = unitOfWork.Set<Product>();
        }

        public async Task<IList<SiteMapLinkViewModel>> GetProductsSiteMap()
        {
            return await _products.OrderByDescending(p => p.PostedDate)
                .Select(p => new SiteMapLinkViewModel
                {
                    Id = p.Id,
                    SlugUrl = p.SlugUrl,
                    LastModified = p.PostedDate
                }).Cacheable().ToListAsync();
        }
    }
}
