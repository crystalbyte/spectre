namespace Crystalbyte.Spectre.Web{
    public interface ISchemeHandlerFactoryDescriptor{
        string SchemeName { get; }
        string DomainName { get; }
        SchemeHandlerFactory Factory { get; }
    }
}