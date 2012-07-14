#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class HtmlRenderer : DisposableObject {
        private readonly ClientHandler _handler;
        private readonly IWindowResizer _resizer;
        private readonly BrowserSettings _settings;
        private readonly IRenderTarget _target;
        private Browser _browser;
        private BrowserHost _browserHost;

        public HtmlRenderer(IRenderTarget target, BrowserDelegate @delegate) {
            _target = target;
            _target.TargetClosing += OnTargetClosing;
            _target.TargetClosed += OnTargetClosed;
            _target.TargetSizeChanged += OnTargetSizeChanged;
            _handler = new ClientHandler(@delegate);
            _settings = new BrowserSettings();
			
			if (Platform.IsOSX) {
				_resizer = new MacWindowResizer();
			}
			
			if (Platform.IsWindows) {
				_resizer =  new WindowsWindowResizer();
			}
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
            _target.TargetClosing -= OnTargetClosing;
            _target.TargetClosed -= OnTargetClosed;
            _target.TargetSizeChanged -= OnTargetSizeChanged;
            _handler.Dispose();
            _browser.Dispose();
            _browserHost.Dispose();
            base.DisposeManaged();
        }

        private void OnTargetClosing(object sender, EventArgs e) {
            _browserHost.ParentWindowWillClose();
        }

        private void OnTargetClosed(object sender, EventArgs e) {
            // nada
        }

        private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) {
            // TODO: the passed down window size is too large, therefor we subtract offsets to 
            var bounds = new Rectangle(0, 0, e.Size.Width - Offsets.WindowRight, e.Size.Height - Offsets.WindowBottom);
            _resizer.Resize(_browserHost.WindowHandle, bounds);
        }

        internal void CreateBrowser() {
            OnCreating(EventArgs.Empty);
            // starts the browser render loop
			var a = new BrowserCreationArgs {
                ClientHandler = _handler,
                Settings = _settings,
                StartUri = _target.StartupUri
            };
			
			if (Platform.IsWindows) {
				a.WindowInfo = new WindowsWindowInfo(_target);
			}
			
			if (Platform.IsOSX) {
				a.WindowInfo = new MacWindowInfo(_target);
			}
			
            _browser = BrowserHost.CreateBrowser(a);
			
            _browserHost = _browser.Host;
            OnCreated(EventArgs.Empty);
            _target.Show();
        }
    }
}