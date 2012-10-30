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
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public sealed class WinformsBootstrapper : Bootstrapper {
        private Window _window;

        protected override void OnStarting(object sender, EventArgs e) {
            _window = new Window();
            _window.GetRenderTargets()
                .ForEach(x => Application.Current.Add(new Viewport(x, new BrowserDelegate())));
            _window.Show();
        }

        protected override IList<Scripting.ScriptingCommand> RegisterScriptingCommands() {
            var commands = base.RegisterScriptingCommands();
            commands.Add(new InfoCommand());
            return commands;
        }

        protected override IEnumerable<Viewport> CreateViewports() {
            // views will be created dynamically
            yield break;
        }
    }
}
