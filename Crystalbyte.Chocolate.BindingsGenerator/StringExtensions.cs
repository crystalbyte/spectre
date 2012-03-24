using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    internal static class StringExtensions
    {
        public static string FirstToUpper(this string input) {
            if (String.IsNullOrEmpty(input))
                return string.Empty;
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static string FirstToLower(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return string.Empty;
            return input.First().ToString().ToLower() + String.Join("", input.Skip(1));
        }
    }
}
