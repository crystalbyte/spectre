#region Using directives

using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre{
    public interface IReadOnlyCollection<out T> : IEnumerable<T>{
        T this[int index] { get; }
        int Count { get; }
    }
}