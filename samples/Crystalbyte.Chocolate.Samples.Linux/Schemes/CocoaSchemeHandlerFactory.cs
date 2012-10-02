using System.ComponentModel.Composition;
using Crystalbyte.Chocolate.Composition;

namespace Crystalbyte.Chocolate.Schemes
{
    [ExportScheme(SchemeName = "cocoa")]
    public sealed class CocoaSchemeHandlerFactory : SchemeHandlerFactory
    {
        protected override ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e) {
            return new CocoaResourceHandler();
        }
    }
}
