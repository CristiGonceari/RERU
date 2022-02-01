using System;
using System.Collections.Generic;
using System.Reflection;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CVU.ERP.Module.Application.TablePrinterService.Validators
{
    public class TablePrinterValidator<T> : AbstractValidator<List<string>>
    {
        public TablePrinterValidator(string errorMessage, string errorCode)
        {
            RuleFor(x => x).Custom((f, c) => ValidateFields(f, errorMessage, errorCode, c));
        }

        private void ValidateFields(List<string> fields, string errorMessage, string errorCode, CustomContext context)
        {

            Type objType = typeof(T);

            foreach (var field in fields)
            {
                var propInfo = GetPropertyInfo(objType, field);

                if (propInfo == null)
                {
                    context.AddFail(errorCode, errorMessage);
                }
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo;
            do
            {
                propInfo = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                type = type.BaseType;
            }
            while (propInfo == null && type != null);
            return propInfo;
        }
    }
}
