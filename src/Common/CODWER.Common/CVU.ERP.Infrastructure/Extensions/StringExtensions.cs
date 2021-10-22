namespace CVU.ERP.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            char[] a = value.ToCharArray();
            a[0] = char.ToUpperInvariant(a[0]);

            return new string(a);
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            char[] a = value.ToCharArray();
            a[0] = char.ToLowerInvariant(a[0]);

            return new string(a);
        }
    }
}
