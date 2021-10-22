using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVU.ERP.Common.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string inputString)
        {
            return inputString.Aggregate(string.Empty, (result, next) =>
            {
                if (char.IsUpper(next) && result.Length > 0)
                {
                    result += ' ';
                }
                return result + next;
            });
        }

        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
