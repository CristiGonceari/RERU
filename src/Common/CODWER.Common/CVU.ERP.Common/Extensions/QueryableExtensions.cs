using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CVU.ERP.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static Expression<Func<T, bool>> GetLikeFunc<T>(string propertyName, string filterText)
        {
            var likeMethod = typeof(DbFunctionsExtensions)
                            .GetMethods()
                            .Where(p => p.Name == "Like")
                            .First();

            ConstantExpression likeConstant = Expression.Constant($"%{filterText}%", typeof(string));
            var memberExpression = Expression.Parameter(typeof(T), "entity");
            var propertyExpression = Expression.Property(memberExpression, propertyName);

            var likeFirstNameMethodCall = Expression.Call(
                    likeMethod,
                    Expression.Constant(EF.Functions, typeof(DbFunctions)),
                    propertyExpression,
                    likeConstant);

            return Expression.Lambda<Func<T, bool>>(likeFirstNameMethodCall, memberExpression);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string column)
        {
            if (string.IsNullOrEmpty(column))
            {
                return source;
            }

            var expression = source.Expression;
            int count = 0;
            var item = column.Split(" ")[0];
            var sort = column.Split(" ").Length > 1 ? column.Split(" ")[1] : "asc";
            if (column != "Id")
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.Property(parameter, item);
                var method = string.Equals(sort, "desc", StringComparison.OrdinalIgnoreCase)
                    ? (count == 0 ? "OrderByDescending" : "ThenByDescending")
                    : (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }

    }
}