#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class Renderer : DisposableObject {
        private readonly ClientHandler _handler;
        private readonly WindowResizer _resizer;
        private readonly RenderSettings _settings;
        private readonly IRenderTarget _target;
        private Browser _browser;
        private BrowserHost _browserHost;

        public Renderer(IRenderTarget target, RenderDelegate @delegate) {
            _target = target;
            _target.TargetClosing += OnTargetClosed;
            _target.TargetSizeChanged += OnTargetSizeChanged;
            _handler = new ClientHandler(@delegate);
            _settings = new RenderSettings();
            _resizer = new WindowResizer();
        }

        public RenderSettings Settings {
            get { return _settings; }
        }

        public IRenderTarget RenderTarget {
            get { return _target; }
        }

        protected override void DisposeManaged() {
            _target.TargetClosing -= OnTargetClosing;
            _target.TargetClosed -= OnTargetClosed;
            _target.TargetSizeChanged -= OnTargetSizeChanged;
            _browserHost.Dispose();
            _browser.Dispose();
            base.DisposeManaged();
        }

        private void OnTargetClosing(object sender, EventArgs e) {
            _browserHost.ParentWindowWillClose();
        }

        private void OnTargetClosed(object sender, EventArgs e) {
            // nothing yet
        }

        private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) {
            var bounds = new Rectangle(0, 0, e.Size.Width, e.Size.Height);
            _resizer.Resize(_browserHost.WindowHandle, bounds);
        }

        internal void CreateBrowser() {
            _browser = BrowserHost.CreateBrowser(new BrowserArgs {
                ClientHandler = _handler,
                Settings = _settings,
                StartUri = _target.StartupUri,
                WindowInfo = new WindowInfo(_target)
            });
            _browserHost = _browser.Host;
            _target.Show();
        }
    }
}