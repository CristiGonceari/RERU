using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CVU.ERP.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayValue(this Enum value)
        {
            var type = value.GetType();
            if (!type.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"Type '{type}' is not Enum", nameof(type));
            }

            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                throw new ArgumentException($"Member '{value}' not found in type '{type.Name}'", nameof(type));
            }

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false).ToList();
            if (!attributes.Any())
            {
                return value.ToString();
            }

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }

        public static string GetDisplayValue(this object value)
        {
            var type = value.GetType();
            if (!type.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"Type '{type}' is not Enum", nameof(type));
            }

            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                throw new ArgumentException($"Member '{value}' not found in type '{type.Name}'", nameof(type));
            }

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false).ToList();
            if (!attributes.Any())
            {
                return value.ToString();
            }

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }

    }
}
