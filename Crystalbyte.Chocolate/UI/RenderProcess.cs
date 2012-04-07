#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class RenderProcess : DisposableObject {
        private readonly ClientHandler _handler;
        private readonly WindowResizer _resizer;
        private readonly BrowserSettings _settings;
        private readonly IRenderTarget _target;
        private Browser _browser;
        private BrowserHost _browserHost;

        public RenderProcess(IRenderTarget target, BrowserDelegate @delegate) {
            _target = target;
            _target.TargetClosing += OnTargetClosing;
            _target.TargetClosed += OnTargetClosed;
            _target.TargetSizeChanged += OnTargetSizeChanged;
            _handler = new ClientHandler(@delegate);
            _settings = new BrowserSettings();
            _resizer = new WindowResizer();
        }

        public BrowserSettings Settings {
            get { return _settings; }
        }

        public IRenderTarget RenderTarget {
            get { return _target; }
        }

        public event EventHandler Creating;

        private void OnCreating(EventArgs e) {
            var handler = Creating;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler Created;

        private void OnCreated(EventArgs e) {
            var handler = Created;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected override void DisposeManaged() {
            base.DisposeManaged();
            _target.TargetClosing -= OnTargetClosing;
            _target.TargetClosed -= OnTargetClosed;
            _target.TargetSizeChanged -= OnTargetSizeChanged;
            _handler.Dispose();
            _browser.Dispose();
            _browserHost.Dispose();
        }

        private void OnTargetClosing(object sender, EventArgs e) {
            _browserHost.ParentWindowWillClose();
        }

        private void OnTargetClosed(object sender, EventArgs e) {
            // nada
        }

        private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) {
            var bounds = new Rectangle(0, 0, e.Size.Width - Offsets.WindowRight, e.Size.Height - Offsets.WindowBottom);
            _resizer.Resize(_browserHost.WindowHandle, bounds);
        }

        internal void CreateBrowser() {
            OnCreating(EventArgs.Empty);
            // starts the browser render loop
            _browser = BrowserHost.CreateBrowser(new BrowserCreationArgs {
                ClientHandler = _handler,
                Settings = _settings,
                StartUri = _target.StartupUri,
                WindowInfo = new WindowInfo(_target)
            });
            _browserHost = _browser.Host;
            OnCreated(EventArgs.Empty);
            _target.Show();
        }
    }
}