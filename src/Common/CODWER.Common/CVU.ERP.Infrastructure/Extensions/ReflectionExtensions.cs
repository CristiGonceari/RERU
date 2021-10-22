using System;
using System.Reflection;

namespace CVU.ERP.Infrastructure.Extensions
{
    public static class ReflectionExtensions
    {
        public static Type GetGenericBaseType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.GetTypeInfo().IsGenericType)
            {
                throw new ArgumentOutOfRangeException(nameof(type), type.FullName + " isn't Generic");
            }

            var args = type.GetGenericArguments();
            if (args.Length != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(type), type.FullName + " isn't a Generic type with one argument -- e.g. T<U>");
            }

            return args[0];
        }

        /// <summary>
        /// [ <c>public static object GetDefault(this Type type)</c> ]
        /// <para></para>
        /// Retrieves the default value for a given Type
        /// </summary>
        /// <param name="type">The Type for which to get the default value</param>
        /// <returns>The default value for <paramref name="type"/></returns>
        /// <remarks>
        /// If a null Type, a reference Type, or a System.Void Type is supplied, this method always returns null.  If a value type 
        /// is supplied which is not publicly visible or which contains generic parameters, this method will fail with an 
        /// exception.
        /// </remarks>
        /// <example>
        /// To use this method in its native, non-extension form, make a call like:
        /// <code>
        ///     object Default = DefaultValue.GetDefault(someType);
        /// </code>
        /// To use this method in its Type-extension form, make a call like:
        /// <code>
        ///     object Default = someType.GetDefault();
        /// </code>
        /// </example>
        public static object GetDefault(this Type type)
        {
            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !type.GetTypeInfo().IsValueType || type == typeof(void))
            {
                return null;
            }

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (type.GetTypeInfo().ContainsGenericParameters)
            {
                throw new ArgumentException("{" + typeof(ReflectionExtensions).GetMethod(nameof(GetDefault)) + "} Error:\n\nThe supplied value type <" + type + "> contains generic parameters, so the default value cannot be retrieved");
            }

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct/enum), return a 
            // default instance of the value type
            if (type.GetTypeInfo().IsPrimitive || !type.GetTypeInfo().IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("{" + typeof(ReflectionExtensions).GetMethod(nameof(GetDefault)) + "} Error:\n\nThe Activator.CreateInstance method could not create a default instance of the supplied value type <" + type + "> (Inner Exception message: \"" + e.Message + "\")", e);
                }
            }

            // Fail with exception
            throw new ArgumentException("{" + typeof(ReflectionExtensions).GetMethod(nameof(GetDefault)) + "} Error:\n\nThe supplied value type <" + type + "> is not a publicly-visible type, so the default value cannot be retrieved");
        }

        /// <summary>
        /// [ <c>public static T GetDefault&lt; T &gt;()</c> ]
        /// <para></para>
        /// Retrieves the default value for a given Type
        /// </summary>
        /// <typeparam name="T">The Type for which to get the default value</typeparam>
        /// <returns>The default value for Type T</returns>
        /// <remarks>
        /// If a reference Type or a System.Void Type is supplied, this method always returns null.  If a value type 
        /// is supplied which is not publicly visible or which contains generic parameters, this method will fail with an 
        /// exception.
        /// </remarks>
        /// <seealso cref="GetDefault(Type)"/>
        public static T GetDefault<T>()
        {
            return (T)GetDefault(typeof(T));
        }

        public static bool HasProperties(this Type type, string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the field are separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // check if the requested fields exist on source
            foreach (var field in fieldsAfterSplit)
            {
                // trim each field, as it might contain leading 
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var propertyName = field.Trim();

                // use reflection to check if the property can be
                // found on T. 
                var propertyInfo = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // it can't be found, return false
                if (propertyInfo == null)
                {
                    return false;
                }
            }

            // all checks out, return true
            return true;
        }

        public static bool HasProperties<T>(string fields)
        {
            return typeof(T).HasProperties(fields);
        }

        public static bool HasProperties(this object obj, string fields)
        {
            return obj.GetType().HasProperties(fields);
        }
    }
}
