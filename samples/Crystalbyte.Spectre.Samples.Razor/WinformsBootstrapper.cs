#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using Crystalbyte.Spectre.Razor;
using Crystalbyte.Spectre.Samples.Controllers;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public sealed class WinformsBootstrapper : Bootstrapper {

        protected override void ConfigureSettings(ApplicationSettings settings) {
#if DEBUG
            settings.IsSingleProcess = true;
#endif
            base.ConfigureSettings(settings);
        }

        protected override void OnStarting(object sender, EventArgs e) {
            ControllerRegistrar.Register("home", typeof(HomeController));
            base.OnStarting(sender, e);
        }

        protected override IEnumerable<Viewport> CreateViewports() {
            yield return new Viewport(
                new Window { StartupUri = new Uri("spectre://localhost/Controllers/Home")}, 
                new BrowserDelegate());
        }

        protected override IList<ISchemeHandlerFactoryDescriptor> RegisterSchemeHandlerFactories() {
            var descriptors = base.RegisterSchemeHandlerFactories();
            var spectre = (SpectreSchemeHandlerFactoryDescriptor)
                descriptors.First(x => x is SpectreSchemeHandlerFactoryDescriptor);
            spectre.Register(typeof(RazorDataProvider));
            return descriptors;
        }
    }
}
