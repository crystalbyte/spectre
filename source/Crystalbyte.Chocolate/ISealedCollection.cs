#region Namespace Directives

using System.Collections.Generic;

#endregion

namespace Crystalbyte.Chocolate {
    public interface ISealedCollection<out T> : IEnumerable<T> {
        T this[int index] { get; }
        int Count { get; }
    }
}