using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using MRJ.DomainClasses;
using MRJ.ViewModels;
using JqGridHelper.DynamicSearch;
using JqGridHelper.Models;

namespace MRJ.ServiceLayer.Contracts
{
    public interface IPageService
    {
        Task<DataGridViewModel<PageDataGridViewModel>> GetDataGridSource(string orderBy, JqGridRequest request,
           NameValueCollection form, DateTimeType dateTimeType, int page, int pageSize);

        void AddPage(Page page, IList<Page> otherPages);
        void DeletePage(int pageId);
        void EditPage(Page page, IList<Page> otherPages);
        Task<IList<AddPageViewModel>> GetAllPagesForAdd();
        Task<AddPageViewModel> GetPage(int pageId);
        Task<IList<LinkViewModel>> GetPageLinks();
        Task<PostViewModel> GetResumePage(string title);
    }
}
