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
using System.Diagnostics;
using System.Windows.Forms;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public sealed class WinformsBootstrapper : Bootstrapper {
		protected override void ConfigureSettings (ApplicationSettings settings) {
			base.ConfigureSettings (settings);
            settings.RemoteDebuggingPort = 9222;
		}

        protected override IEnumerable<Viewport> CreateViewports() {
            yield return new Viewport(
                new Window { StartupUri = new Uri("http://peacekeeper.futuremark.com/") },
                new BrowserDelegate());
        }

        protected override void OnStarting(object sender, EventArgs e) {
            base.OnStarting(sender, e);

            Process.Start("http://localhost:9222");
        }
    }
}
