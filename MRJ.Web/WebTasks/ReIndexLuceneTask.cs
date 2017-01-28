﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using DNTScheduler;
using MRJ.LuceneSearch;
using MRJ.ServiceLayer.Contracts;
using MRJ.ViewModels;
using MRJ.Web.DependencyResolution;
using Utilities;

namespace MRJ.Web.WebTasks
{
    public class ReIndexLuceneTask : ScheduledTaskTemplate
    {
        /// <summary>
        /// اگر چند جاب در يك زمان مشخص داشتيد، اين خاصيت ترتيب اجراي آن‌ها را مشخص خواهد كرد
        /// </summary>
        public override int Order => 1;

        public override bool RunAt(DateTime utcNow)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;

            var now = utcNow.AddHours(3.5);

            return now.Hour == 2 &&
                   now.Minute == 1 && now.Second == 1;
        }

        public override async Task RunAsync()
        {
            if (this.IsShuttingDown || this.Pause)
                return;

            LuceneIndex.ClearLuceneIndex();

            var productService = IoC.Container.GetInstance<IProductService>();
            var postService = IoC.Container.GetInstance<IPostService>();

            foreach (var product in await productService.GetAllForLuceneIndex())
            {
                LuceneIndex.ClearLuceneIndexRecord(product.Id);
                LuceneIndex.AddUpdateLuceneIndex(new LuceneSearchModel
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Image = product.Image,
                    Description = product.Description.RemoveHtmlTags(),
                    Category = "کالا‌ها",
                    SlugUrl = product.SlugUrl,
                    Price = product.Price.ToString(CultureInfo.InvariantCulture),
                    ProductStatus = product.ProductStatus.ToString()
                });
            }

            foreach (var post in await postService.GetAllForLuceneIndex())
            {
                LuceneIndex.ClearLucenePostIndexRecord(post.Id);
                LuceneIndex.AddUpdateLuceneIndex(new LuceneSearchModel
                {
                    PostId = post.Id,
                    Title = post.Title,
                    Image = post.Image,
                    Description = post.Description.RemoveHtmlTags(),
                    Category = post.Category,
                    SlugUrl = post.SlugUrl
                });
            }
        }

        public override string Name => "ReIndexLuceneTask";
    }

}