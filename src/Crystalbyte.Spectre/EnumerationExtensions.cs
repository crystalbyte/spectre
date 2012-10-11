#region Using directives

using System;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre{
    public static class EnumerationExtensions{
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action){
            foreach (var item in items){
                action(item);
            }
        }

        public static void AddRange<T>(this IList<T> target, IEnumerable<T> items){
            foreach (var item in items){
                target.Add(item);
            }
        }
    }
}