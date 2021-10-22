using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CVU.ERP.Common.Extensions
{
    public static class ReflectionExtensions
    {
        public static List<String> GetPropNames<T>(params Expression<Func<T, string>>[] navigationProperties)
        {
            var result = new List<String>();
            foreach (var navigationProperty in navigationProperties)
            {
                var member = navigationProperty.Body as MemberExpression;
                if (member == null)
                {
                    throw new ArgumentException();
                }
                result.Add(member.Member.Name);
            }
            return result;
        }
    }
}