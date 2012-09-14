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

            _settings = new BrowserSettings {
                IsFileAccessfromUrlsAllowed = false,
                IsWebSecurityDisabled = false,
                IsUniversalAccessFromFileUrlsAllowed = false,
                IsUserStyleSheetEnabled = false
            };

            if (Platform.IsLinux) {
                _resizer = new LinuxWindowResizer();
            }

            if (Platform.IsWindows) {
                _resizer = new WindowsWindowResizer();
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
            var bounds = new Rectangle(0, 0, e.Size.Width, e.Size.Height);
            _resizer.Resize(_browserHost.WindowHandle, bounds);
        }

        internal void CreateBrowser() {
            OnCreating(EventArgs.Empty);

            // Starts the browser rendering loop.
            var a = new BrowserCreationArgs {
                ClientHandler = _handler,
                Settings = _settings,
                StartUri = _target.StartupUri
            };

            if (Platform.IsWindows) {
                a.WindowInfo = new WindowsWindowInfo(_target);
            }

            if (Platform.IsLinux) {
                a.WindowInfo = new LinuxWindowInfo(_target);
            }

            _browser = BrowserHost.CreateBrowser(a);

            _browserHost = _browser.Host;
            OnCreated(EventArgs.Empty);
            _target.Show();
        }
    }
}