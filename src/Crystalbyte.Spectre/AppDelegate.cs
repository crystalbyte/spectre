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
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    public class AppDelegate {
        public event EventHandler<BrowserEventArgs> BrowserDestroyed;

        protected internal virtual void OnBrowserDestroyed(BrowserEventArgs e) {
            var handler = BrowserDestroyed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ProcessStartedEventArgs> ProcessStarted;

        protected internal virtual void OnProcessStarted(ProcessStartedEventArgs e) {
            var handler = ProcessStarted;
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
    }
}
