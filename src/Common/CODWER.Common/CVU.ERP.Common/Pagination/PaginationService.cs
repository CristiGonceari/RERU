using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CVU.ERP.Common.Pagination
{
    public class PaginationService : IPaginationService
    {
        private readonly IMapper _mapper;
        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginatedList<T> PaginatedResults<T>(IQueryable<T> queryableList, PaginatedQueryParameter pagedQuery, Expression<Func<T, string>>[] membersToSearch = null)
        {
            queryableList = queryableList.FilterAndSort(pagedQuery, membersToSearch);

            return PaginatedList<T>.Create(queryableList, pagedQuery.Page, pagedQuery.ItemsPerPage);
        }

        public PaginatedList<TSource, TDestination> PaginatedResults<TSource, TDestination>(IQueryable<TSource> queryableList, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, string>>[] membersToSearch = null)
        {
            queryableList = queryableList.FilterAndSort(pagedQuery, membersToSearch);

            return PaginatedList<TSource, TDestination>.Create(queryableList, pagedQuery.Page, pagedQuery.ItemsPerPage);
        }
        
        public PaginatedModel<TDestination> MapAndPaginateModel<TSource, TDestination>(IQueryable<TSource> queryableList, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, string>>[] membersToSearch = null)
        {
            queryableList = queryableList.FilterAndSort(pagedQuery, membersToSearch);

            var mappedItems = _mapper.Map<List<TDestination>>(queryableList);
            var paginatedList = PaginatedList<TDestination>.Create(mappedItems, pagedQuery.Page, pagedQuery.ItemsPerPage);

            return new PaginatedModel<TDestination>(paginatedList);
        }

        public async Task<PaginatedModel<TDestination>> MapAndPaginateModelAsync<TSource, TDestination>(IQueryable<TSource> queryableList, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, string>>[] membersToSearch = null)
        {
            var list = await queryableList.FilterAndSort(pagedQuery, membersToSearch).ToListAsync();

            var mappedItems = _mapper.Map<List<TDestination>>(list);
            var paginatedList = PaginatedList<TDestination>.Create(mappedItems, pagedQuery.Page, pagedQuery.ItemsPerPage);

            return new PaginatedModel<TDestination>(paginatedList);
        }

        public PaginatedModel<TSource> MapAndPaginateModel<TSource>(List<TSource> list, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, string>>[] membersToSearch = null)
        {
            var paginatedList = PaginatedList<TSource>.Create(list, pagedQuery.Page, pagedQuery.ItemsPerPage);

            return new PaginatedModel<TSource>(paginatedList);
        }
    }
}