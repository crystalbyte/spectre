using System;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    public sealed class View : DisposableObject {
        private readonly ClientHandler _handler;
        private readonly IRenderTarget _target;
        private readonly ViewSettings _settings;
        private readonly WindowResizer _resizer;
        private Browser _browser;

        public View(IRenderTarget target, ViewDelegate @delegate) {
            _target = target;
            _target.TargetClosing += OnTargetClosed;
            _target.TargetSizeChanged += OnTargetSizeChanged;
            _handler = new ClientHandler(@delegate);
            _settings = new ViewSettings();
            _resizer = new WindowResizer();
        }

        protected override void DisposeManaged() {
            _target.TargetClosing -= OnTargetClosing;
            _target.TargetClosed -= OnTargetClosed;
            _target.TargetSizeChanged -= OnTargetSizeChanged;
            _browser.Dispose();
            base.DisposeManaged();
        }

        private void OnTargetClosing(object sender, EventArgs e) {
            _browser.ParentWindowWillClose();                                   
        }

        private void OnTargetClosed(object sender, EventArgs e) {
            // nothing yet
        }

        private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) {
            var bounds = new Rectangle(0, 0, e.Size.Width, e.Size.Height);
            _resizer.Resize(_browser.WindowHandle, bounds);
        }

        internal void CreateBrowser() {
            _browser = new Browser(new BrowserArgs {
                ClientHandler = _handler,
                Settings = _settings,
                StartUri = _target.StartupUri,
                WindowInfo = new WindowInfo(_target)
            });
        }

        public ViewSettings Settings {
            get { return _settings; }
        }

        public IRenderTarget RenderTarget { get { return _target; } }
    }
}
