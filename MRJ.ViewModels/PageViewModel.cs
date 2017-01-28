using System.Collections.Generic;
using AutoMapper;
using MRJ.DomainClasses;

namespace MRJ.ViewModels
{
    public class AddPageViewModel : AddPostViewModel
    {
        public AddPageViewModel()
        {
            Pages = new List<AddPageViewModel>();
        }

        public int Order { get; set; }

        public IList<AddPageViewModel> Pages { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            

            configuration.CreateMap<Page, AddPageViewModel>().ForMember(x => x.Tags, opt => opt.Ignore()).IncludeBase<Post, AddPostViewModel>();


            configuration.CreateMap<AddPageViewModel, Page>().ForMember(x=>x.Tags,opt=>opt.Ignore()).IncludeBase<AddPostViewModel, Post>();


        }
    }
}
