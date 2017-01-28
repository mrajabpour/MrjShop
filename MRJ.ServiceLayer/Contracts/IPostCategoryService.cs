using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using MRJ.DomainClasses;
using MRJ.ViewModels;
using JqGridHelper.DynamicSearch;
using JqGridHelper.Models;

namespace MRJ.ServiceLayer.Contracts
{
    public interface IPostCategoryService
    {
        Task<DataGridViewModel<CategoryDataGridViewModel>> GetDataGridSource(string orderBy, JqGridRequest request,
              NameValueCollection form, DateTimeType dateTimeType, int page, int pageSize);

        void Add(PostCategory category);
        void Delete(int id);
        void Edit(PostCategory category);
        Task<IList<LinkViewModel>> GetCategoryLinks();
        Task<IList<CategoryViewModel>> GetAll();
        Task<IList<PostCategorySideBarViewModel>> GetSideBar();
        Task<string> GetCategoryName(int id);
    }
}
