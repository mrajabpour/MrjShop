using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MRJ.DataLayer;
using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using MRJ.ViewModels;

namespace MRJ.ServiceLayer
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IDbSet<Product> _products;

        public AdminPanelService(IUnitOfWork unitOfWork)
        {
            _products = unitOfWork.Set<Product>();
        }

        public async Task<AdminDashboardViewModel> GetDashboardStatistics()
        {
            return new AdminDashboardViewModel
            {
                AvailableProductsCount =
                    await _products.Where(p => p.ProductStatus == ProductStatus.Available).CountAsync(),
                UnAvailableProductsCount =
                    await _products.Where(p => p.ProductStatus == ProductStatus.NotAvailable).CountAsync(),
                TotalProductsCount = await _products.CountAsync()
            };
        }
    }
}
