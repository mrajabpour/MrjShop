using AutoMapper;
using MRJ.DomainClasses;

namespace MRJ.ViewModels
{
    public class PageDataGridViewModel : PostDataGridViewModel
    {
        public int Order { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            base.CreateMappings(configuration);

            configuration.CreateMap<Page, PageDataGridViewModel>();
        }
    }
}
