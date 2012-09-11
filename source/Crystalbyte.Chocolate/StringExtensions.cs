using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    internal static class StringExtensions
    {
        public static string ToFileExtension(this string input) {
            if (input == null) {
                throw new ArgumentNullException("input");
            }

            return input.Split('.').LastOrDefault();
        }
    }
}
