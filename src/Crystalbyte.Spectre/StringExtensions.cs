#region Using directives

using System;
using System.Linq;

#endregion

namespace Crystalbyte.Spectre{
    internal static class StringExtensions{
        public static string ToFileExtension(this string input){
            if (input == null){
                throw new ArgumentNullException("input");
            }

            return input.Split('.').LastOrDefault();
        }
    }
}