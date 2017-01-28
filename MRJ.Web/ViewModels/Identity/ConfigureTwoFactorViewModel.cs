﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace MRJ.Web.ViewModels.Identity
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }

}