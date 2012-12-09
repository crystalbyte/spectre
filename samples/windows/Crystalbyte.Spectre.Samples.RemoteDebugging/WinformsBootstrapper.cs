#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public sealed class WinformsBootstrapper : Bootstrapper {
        protected override void ConfigureSettings(ApplicationSettings settings) {
            base.ConfigureSettings(settings);
            settings.RemoteDebuggingPort = 9222;
        }

        protected override IEnumerable<Viewport> CreateViewports() {
            yield return new Viewport(
                new Window {StartupUri = new Uri("http://peacekeeper.futuremark.com/")},
                new BrowserDelegate());
        }

        protected override void OnStarting(object sender, EventArgs e) {
            base.OnStarting(sender, e);

            Process.Start("http://localhost:9222");
        }
    }
}
