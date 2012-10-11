namespace Crystalbyte.Spectre.Web{
    public interface ISchemeDescriptor{
        string Scheme { get; }
        SchemeProperties SchemeProperties { get; }
    }
}