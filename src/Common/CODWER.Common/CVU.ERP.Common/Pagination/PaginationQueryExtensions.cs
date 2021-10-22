using CVU.ERP.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CVU.ERP.Common.Pagination
{
    public static class PaginationQueryExtensions
    {
        public static  IQueryable<T> FilterAndSort<T>(this IQueryable<T> queryableList, PaginatedQueryParameter pagedQuery, Expression<Func<T, string>>[] membersToSearch = null)
        {
            var propertyFieldsToSearch = new List<string>();

            if (pagedQuery == null)
            {
                pagedQuery = new PaginatedQueryParameter();
            }

            // If memberToSearch is passed, then use the properties supplied to be filtered
            if (membersToSearch != null)
            {
                propertyFieldsToSearch = ReflectionExtensions.GetPropNames(membersToSearch);
            }
            else
            { // Else get all string properties through reflection and use those
                propertyFieldsToSearch = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x =>
                        !x.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && x.GetType() == typeof(string) &&
                                        (propertyFieldsToSearch.Contains(x.Name) || propertyFieldsToSearch.Count == 0))
                    .Select(f => f.Name).ToList();
            }

            // Order query by supplied field
            queryableList = queryableList.OrderBy(pagedQuery.OrderField);

            // Filter by input
            if (!string.IsNullOrEmpty(pagedQuery.SearchBy))
            {
                // Search multi-word text
                var searchQueries = pagedQuery.SearchBy.Split(' ');
                var filterExpressions = new List<Expression<Func<T, bool>>>();

                foreach (var searchQuery in searchQueries)
                {
                    // trim & ignore casing
                    var searchQueryForWhereClause = searchQuery.Trim().ToLowerInvariant();

                    // Search word in properties and generate like query functions
                    foreach (var prop in propertyFieldsToSearch)
                    {
                        filterExpressions.Add(QueryableExtensions.GetLikeFunc<T>(prop, searchQuery));
                    }
                }

                // If we have filters, aggregate them with OR and filter list
                if (filterExpressions.Count > 0)
                {
                    var filterExpression = filterExpressions.Aggregate(PredicateBuilder.Or);
                    queryableList = queryableList.Where(filterExpression);
                }
            }

            return queryableList;
        }

    }
}
