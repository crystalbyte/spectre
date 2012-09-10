#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate {
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