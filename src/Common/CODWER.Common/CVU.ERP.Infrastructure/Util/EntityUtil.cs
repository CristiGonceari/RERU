using System;
using System.Linq.Expressions;

namespace CVU.ERP.Infrastructure.Util
{
    public static class EntityUtil
    {
        public static Expression<Func<T, bool>> CreateEqualsExpression<T, TValue>(string propertyName, TValue propertyValue) where TValue : IComparable, IConvertible, IEquatable<TValue>
        {
            Type type = typeof(T);
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var parameterExpression = Expression.Parameter(typeof(T), "e");
                var memberExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);
                var constantExpression = Expression.Constant(propertyValue, propertyValue.GetType());
                var binaryExpression = Expression.Equal(memberExpression, constantExpression);

                return Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression);
            }
            else
            {
                return null;
            }
        }

        public static Expression<Func<T, bool>> CreateGreaterThanExpression<T, TValue>(string propertyName, TValue propertyValue)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var parameterExpression = Expression.Parameter(typeof(T), "e");
                var memberExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);
                var constantExpression = Expression.Constant(propertyValue, propertyValue.GetType());
                var binaryExpression = Expression.GreaterThan(memberExpression, constantExpression);

                return Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression);
            }

            return null;
        }

        public static Expression<Func<T, bool>> CreateGreaterThanOrEqualExpression<T, TValue>(string propertyName, TValue propertyValue)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var parameterExpression = Expression.Parameter(typeof(T), "e");
                var memberExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);
                var constantExpression = Expression.Constant(propertyValue, propertyValue.GetType());
                var binaryExpression = Expression.GreaterThanOrEqual(memberExpression, constantExpression);

                return Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression);
            }

            return null;
        }

        public static Expression<Func<T, TValue>> GetProperty<T, TValue>(string propertyName)
        {
            var type = typeof(T);
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var parameterExpression = Expression.Parameter(typeof(T), "f");
                var memberExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);

                return Expression.Lambda<Func<T, TValue>>(memberExpression, parameterExpression);
            }

            return null;
        }
    }
}
