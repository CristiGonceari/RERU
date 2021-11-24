using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVU.ERP.Common.Pagination
{
    public interface IPaginationService
    {
        PaginatedList<T> PaginatedResults<T>(IQueryable<T> list, PaginatedQueryParameter query, Expression<Func<T, string>>[] membersToSearch = null);
        PaginatedList<TSource, TDestination> PaginatedResults<TSource, TDestination>(IQueryable<TSource> list, PaginatedQueryParameter query, Expression<Func<TSource, string>>[] membersToSearch = null);
        PaginatedModel<TDestination> MapAndPaginateModel<TSource, TDestination>(IQueryable<TSource> list, PaginatedQueryParameter query, Expression<Func<TSource, string>>[] membersToSearch = null);
        Task<PaginatedModel<TDestination>> MapAndPaginateModelAsync<TSource, TDestination>(IQueryable<TSource> list, PaginatedQueryParameter query, Expression<Func<TSource, string>>[] membersToSearch = null);
        PaginatedModel<TSource> MapAndPaginateModel<TSource>(List<TSource> list, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, string>>[] membersToSearch = null);
    }
}