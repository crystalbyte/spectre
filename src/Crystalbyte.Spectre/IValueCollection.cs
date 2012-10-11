namespace Crystalbyte.Spectre{
    public interface IValueCollection<T> : IReadOnlyCollection<T>{
        void Add(T item);
        void Clear();
        bool TryGetValue(int index, out string value);
    }
}