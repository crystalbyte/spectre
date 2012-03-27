using System;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    public sealed class View : DisposableObject {
        private readonly ClientHandler _handler;
        private readonly IRenderTarget _target;
        private readonly ViewSettings _settings;
        private readonly WindowResizer _resizer;
        private WindowInfo _windowInfo;
        private Browser _browser;

        public View(IRenderTarget target, ViewDelegate @delegate) {
            _target = target;
            _target.Closed += OnTargetClosed;
            _target.SizeChanged += OnTargetSizeChanged;
            _handler = new ClientHandler(@delegate);
            _settings = new ViewSettings();
            _resizer = new WindowResizer();
        }

        private void OnTargetClosed(object sender, EventArgs e) {
            
        }

        private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) {
            _resizer.Resize(_windowInfo.WindowHandle, new Rectangle(0, 0, e.Size.Width, e.Size.Height));
        }

        internal void CreateBrowser() {
            _windowInfo = new WindowInfo(_target);
            _browser = new Browser(new BrowserArgs {
                ClientHandler = _handler,
                Settings = _settings,
                StartUri = _target.StartupUri,
                WindowInfo = _windowInfo
            });
        }

        public ViewSettings Settings {
            get { return _settings; }
        }

        public IRenderTarget RenderTarget { get { return _target; } }
    }
}
