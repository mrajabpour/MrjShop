using System.Collections.Generic;
using MRJ.ViewModels;

namespace MRJ.Web.ViewModels
{
    public class ProductSliderWidgetViewModel
    {
        public string Title { get; set; }
        public string CarouselId { get; set; }
        public string LinkText { get; set; }
        public string Link { get; set; }
        public IList<ProductWidgetViewModel> Products { get; set; }
    }
}