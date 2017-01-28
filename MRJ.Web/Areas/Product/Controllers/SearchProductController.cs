﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using MRJ.ServiceLayer.Contracts;
using MRJ.ViewModels;
using PagedList;

namespace MRJ.Web.Areas.Product.Controllers
{
    [RouteArea("Product", AreaPrefix = "product")]
    public partial class SearchProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMappingEngine _mappingEngine;

        public SearchProductController(ICategoryService categoryService, IMappingEngine mappingEngine, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mappingEngine = mappingEngine;
        }

        [Route("Search")]
        public virtual async Task<ActionResult> Index()
        {
            var model = new SerachProductIndexViewModel
            {
                Categories = new GroupsViewModel
                {
                    SelectedGroups = new List<GroupViewModel>(),
                    AvailableGroups = _mappingEngine.Map<IList<CategoryViewModel>, IList<GroupViewModel>>((await _categoryService.GetSearchProductsCategories())),
                },
                Prices = await _productService.GetAvailableProductPrices()
            };

            return View(model);
        }

        [Route("GetProducts")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> GetProducts(SearchProductViewModel model)
        {
            var products = await _productService.SearchProduct(model);

            return PartialView(MVC.Product.SearchProduct.Views._GetProducts,
                               products.ToPagedList(model.PageNumber, model.PageSize));
        }
    }
}