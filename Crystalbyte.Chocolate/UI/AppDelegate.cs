#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate.UI {
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

        protected internal virtual void OnContextCreated(ContextEventArgs e) {
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

        public event EventHandler<ContextEventArgs> ContextReleased;

        protected internal virtual void OnContextReleased(ContextEventArgs e) {
            var handler = ContextReleased;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<IpcMessageReceivedEventArgs> IpcMessageReceived;

        protected internal virtual void OnIpcMessageReceived(IpcMessageReceivedEventArgs e)
        {
            var handler = IpcMessageReceived;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}