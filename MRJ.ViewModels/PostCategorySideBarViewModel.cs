using System.Collections.Generic;
using AutoMapper;
using AutoMapperContracts;
using MRJ.DomainClasses;

namespace MRJ.ViewModels
{
    public class PostCategorySideBarViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<LinkViewModel> Posts { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PostCategory, PostCategorySideBarViewModel>();

        }
    }
}
