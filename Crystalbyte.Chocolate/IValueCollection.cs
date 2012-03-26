namespace Crystalbyte.Chocolate {
    public interface IValueCollection<T> : ISealedCollection<T> {
        void Add(T item);
        void Clear();
        bool TryGetValue(int index, out string value);
    }
}