using System.Collections.Generic;

namespace MRJ.ViewModels
{
    public class FooterViewModel
    {
        public IList<LinkViewModel> PageLinks { get; set; }
        public IList<LinkViewModel> PostCategoryLinks { get; set; }
    }
}
