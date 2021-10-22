using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CVU.ERP.Common.Pagination
{
    public abstract class IPaginationEntity<TSource, TDestination>
    {
        Expression<Func<TSource, TDestination>> Projection { get; }
    }
}
