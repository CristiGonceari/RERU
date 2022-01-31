using System;
using System.Collections.Generic;
using System.Reflection;

namespace CVU.ERP.Module.Application.Services.Implementations
{
    public class TablePrinter<T> : ITablePrinter<T> where T : class
    {
        public string FormatTable(List<T> items, List<string> fields)
        {
            var result = GetTableHeader(fields);
            result += GetTableContent(items, fields);
            throw new NotImplementedException();
        }

        private string GetTableHeader(List<string> fields)
        {
            return "";
        }

        private string GetTableContent(List<T> items, List<string> fields)
        {
            var records = string.Empty;

            foreach (var item in items)
            {
                foreach (var field in fields)
                {
                    records += item.GetPropertyValue(field);
                }
            }

            return records;
        }
    }

    public static class LocalExtensions
    {
        public static dynamic GetPropertyValue<TEntity>(this TEntity entity, string fieldName = "Id")
        {
            entity.CheckEntity();

            Type objType = entity.GetType();
            PropertyInfo propInfo = GetPropertyInfo(objType, fieldName);

            propInfo.CheckProperty(fieldName, objType);

            return propInfo.GetValue(entity, null);
        }

        private static void CheckEntity<TEntity>(this TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
        }

        private static void CheckProperty(this PropertyInfo propInfo, string fieldName, Type objType)
        {
            if (propInfo == null)
            {
                throw new ArgumentOutOfRangeException("fieldName",
                    string.Format($"Couldn't find property {fieldName} in type {objType.FullName}"));
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo;
            do
            {
                propInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (propInfo == null && type != null);
            return propInfo;
        }
    }
}
