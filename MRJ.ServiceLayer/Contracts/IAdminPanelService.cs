using System.Threading.Tasks;
using MRJ.ViewModels;

namespace MRJ.ServiceLayer.Contracts
{
    public interface IAdminPanelService
    {
        Task<AdminDashboardViewModel> GetDashboardStatistics();
    }
}
