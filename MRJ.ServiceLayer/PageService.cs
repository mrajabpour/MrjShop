﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFSecondLevelCache;
using MRJ.DataLayer;
using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using MRJ.ViewModels;
using JqGridHelper.DynamicSearch;
using JqGridHelper.Models;

namespace MRJ.ServiceLayer
{
    public class PageService : IPageService
    {
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Page> _pages;

        public PageService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _pages = unitOfWork.Set<Page>();
            _mappingEngine = mappingEngine;
        }

        public async Task<DataGridViewModel<PageDataGridViewModel>> GetDataGridSource(string orderBy, JqGridRequest request, NameValueCollection form, DateTimeType dateTimeType,
            int page, int pageSize)
        {
            var query = _pages.AsQueryable();

            query = new JqGridSearch(request, form, dateTimeType).ApplyFilter(query);

            var dataGridModel = new DataGridViewModel<PageDataGridViewModel>
            {
                Records = await query.AsQueryable().OrderBy(orderBy)
                    .Skip(page * pageSize)
                    .Take(pageSize).ProjectTo<PageDataGridViewModel>(null, _mappingEngine).ToListAsync(),

                TotalCount = await query.AsQueryable().OrderBy(orderBy).CountAsync()
            };

            return dataGridModel;
        }

        private void FixOrder(IList<Page> otherPages)
        {
            foreach (var page in otherPages)
            {
                _pages.Attach(page);
                _unitOfWork.Entry(page).Property(p => p.Order).IsModified = true;
            }
        }

        public void AddPage(Page page, IList<Page> otherPages)
        {
            _pages.Add(page);

            FixOrder(otherPages);
        }

        public void DeletePage(int pageId)
        {
            var entity = new Page() { Id = pageId };

            _unitOfWork.Entry(entity).State = EntityState.Deleted;
        }

        public void EditPage(Page page, IList<Page> otherPages)
        {
            _pages.Attach(page);
            _unitOfWork.Entry(page).State = EntityState.Modified;
            _unitOfWork.Entry(page).Property(p => p.ViewNumber).IsModified = false;

            FixOrder(otherPages);
        }

        public async Task<IList<AddPageViewModel>> GetAllPagesForAdd()
        {
            return await _pages
                         .OrderBy(page => page.Order).ThenByDescending(page => page.PostedDate)
                             .Select(page => new AddPageViewModel
                             {
                                 Id = page.Id,
                                 Title = page.Title,
                                 Order = page.Order
                             }).ToListAsync();
        }

        public async Task<AddPageViewModel> GetPage(int pageId)
        {
            return await _pages.Where(page => page.Id == pageId)
               .ProjectTo<AddPageViewModel>().FirstOrDefaultAsync();
        }

        public async Task<IList<LinkViewModel>> GetPageLinks()
        {
            return await _pages.OfType<Page>().AsNoTracking().OrderBy(p => p.Order).ProjectTo<LinkViewModel>(null, _mappingEngine)
                .Cacheable()
                .ToListAsync();
        }

        public async Task<PostViewModel> GetResumePage(string title)
        {
            var selectedPage = await _pages.Where(post => post.Title == title)
                 .ProjectTo<PostViewModel>(null, _mappingEngine).FirstOrDefaultAsync();

            var postEntity = new Page
            {
                Id = selectedPage.Id,
                ViewNumber = ++selectedPage.ViewNumber,
            };

            _pages.Attach(postEntity);
            _unitOfWork.Entry(postEntity).Property(p => p.ViewNumber).IsModified = true;

            return selectedPage;
        }
    }
}
