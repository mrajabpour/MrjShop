using AutoMapper;
using AutoMapperContracts;
using MRJ.DomainClasses;

namespace MRJ.ViewModels
{
    public class CategoryViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>();
            configuration.CreateMap<PostCategory, CategoryViewModel>();
            configuration.CreateMap<CategoryViewModel, GroupViewModel>();
        }
    }
}
