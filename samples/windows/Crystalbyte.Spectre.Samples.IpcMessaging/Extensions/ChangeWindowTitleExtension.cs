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

using System.Linq;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.Samples.Extensions {
    internal class ChangeWindowTitleExtension : Extension {
        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "changeWindowTitle", "title"); }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var title = e.Arguments.First().ToString();
            if (string.IsNullOrWhiteSpace(title)) {
                // Chromium does not allow to send nothing over the wire, so we send the termination symbol instead.
                title = "\0";
            }

            var browser = ScriptingContext.Current.Browser;
            browser.SendIpcMessage(ProcessType.Browser, new IpcMessage("change-window-title") {
                Payload = title.ToUtf8Stream()
            });

            e.IsHandled = true;
        }
    }
}
