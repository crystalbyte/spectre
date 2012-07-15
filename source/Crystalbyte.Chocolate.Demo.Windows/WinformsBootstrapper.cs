using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Composition;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate
{
    public sealed class WinformsBootstrapper : MefBootstrapper {
        protected override IRenderTarget CreateRenderTarget() {
            var index = new Uri("http://trailers.apple.com/trailers/fox/prometheus/");
            return new Window { StartupUri = index };
        }

        public override void InitializeCatalog(AggregateCatalog catalog)
        {
            base.InitializeCatalog(catalog);
            catalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
        }
    }
}
