using System;
using System.Collections.Generic;
using Crystalbyte.Spectre.UI;

namespace Crystalbyte.Spectre.Tests.Mono {
    class WinformsBootstrapper : Bootstrapper {
        protected override void ConfigureSettings(ApplicationSettings settings) {
            base.ConfigureSettings(settings);
            //settings.IsSingleProcess = true;
            //settings.ProductVersion = "1.0.0.0";
            //settings.UserAgent = "Spectre";
            //settings.CacheDirectory = "CacheDir";
            //settings.ResourceDirectory = "ResourceDir";
            //settings.BrowserSubprocessPath = "mono.exe";
        }
        protected override IEnumerable<Viewport> CreateViewports() {
            yield return new Viewport(new MainWindow {
                StartupUri = new Uri("http://www.google.com")
            }, new BrowserDelegate());
        }
    }
}
