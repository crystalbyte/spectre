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
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    public class RenderDelegate {
        public event EventHandler<BrowserEventArgs> BrowserDestroyed;

        protected internal virtual void OnBrowserDestroyed(BrowserEventArgs e) {
            var handler = BrowserDestroyed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<CommandLineProcessingEventArgs> CommandLineProcessing;

        protected internal virtual void OnCommandLineProcessing(CommandLineProcessingEventArgs e) {
            var handler = CommandLineProcessing;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ContextEventArgs> ScriptingContextCreated;

        protected internal virtual void OnScriptingContextCreated(ContextEventArgs e) {
            var handler = ScriptingContextCreated;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ProxyUrlEventArgs> ProxyForUrlRequested;

        protected internal virtual void OnProxyForUrlRequested(ProxyUrlEventArgs e) {
            var handler = ProxyForUrlRequested;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<CustomSchemesRegisteringEventArgs> CustomSchemesRegistering;

        public void OnCustomSchemesRegistering(CustomSchemesRegisteringEventArgs e) {
            var handler = CustomSchemesRegistering;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler RenderThreadCreated;

        protected internal virtual void OnRenderThreadCreated(EventArgs e) {
            var handler = RenderThreadCreated;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler Initialized;

        protected internal virtual void OnInitialized(EventArgs e) {
            var handler = Initialized;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BrowserEventArgs> BrowserCreated;

        protected internal virtual void OnBrowserCreated(BrowserEventArgs e) {
            var handler = BrowserCreated;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ContextEventArgs> ScriptingContextReleased;

        protected internal virtual void OnScriptingContextReleased(ContextEventArgs e) {
            var handler = ScriptingContextReleased;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<IpcMessageReceivedEventArgs> IpcMessageReceived;

        protected internal virtual void OnIpcMessageReceived(IpcMessageReceivedEventArgs e) {
            var handler = IpcMessageReceived;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ProcessLaunchingEventArgs> ChildProcessLaunching;

        protected internal virtual void OnChildProcessLaunching(ProcessLaunchingEventArgs e) {
            var handler = ChildProcessLaunching;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}
