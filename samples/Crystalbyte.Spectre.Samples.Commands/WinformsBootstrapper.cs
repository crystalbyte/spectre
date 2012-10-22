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
using Crystalbyte.Spectre.Samples.Commands;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public sealed class WinformsBootstrapper : Bootstrapper {
        protected override void ConfigureSettings(ApplicationSettings settings) {
            // In order to be able to debug commands, we will start the application in SP mode.
            // Without SP mode, all rendering will be outsourced into a seperate process.
            // SP mode is not for production use, for it is not actively maintained by the chromium project.
            // PS. Kitten will die if you use it !
            settings.IsSingleProcess = true;
            base.ConfigureSettings(settings);
        }

        protected override IList<ScriptingCommand> RegisterScriptingCommands() {
            var extensions = base.RegisterScriptingCommands();
            extensions.Add(new IncrementCommand());
            extensions.Add(new ConcurrentIncrementCommand());
            return extensions;
        }

        protected override IEnumerable<Viewport> CreateViewports() {
            yield return new Viewport(
                new Window { StartupUri = new Uri("spectre://localhost/Views/index.html") },
                new BrowserDelegate());    
        }
    }
}
