namespace Crystalbyte.Spectre.Threading {
    public interface ISynchronizable {
        void Lock();
        void Unlock();
    }
}