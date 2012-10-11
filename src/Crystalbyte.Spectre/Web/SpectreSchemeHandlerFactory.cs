#region Using directives

using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class SpectreSchemeHandlerFactory : SchemeHandlerFactory{
        public SpectreSchemeHandlerFactory(){
            Providers = new DataProviders();
            Providers.Register(typeof (FileDataProvider));
        }

        public DataProviders Providers { get; private set; }

        protected override ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e){
            return new SpectreSchemeHandler(Providers.Types);
        }
    }
}