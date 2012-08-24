using System;
using System.ComponentModel.Composition.Hosting;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate
{
    public sealed class WinformsBootstrapper : Bootstrapper {
        protected override IRenderTarget CreateRenderTarget() {
            return new Window { StartupUri = new Uri("http://trailers.apple.com/trailers/fox/prometheus/") };
        }
    }
}
