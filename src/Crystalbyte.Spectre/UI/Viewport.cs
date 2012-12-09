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
using Crystalbyte.Spectre.Interop;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class Viewport : DisposableObject {
        private readonly ClientHandler _handler;
        private readonly IWindowResizer _resizer;
        private readonly BrowserSettings _settings;
        private readonly IRenderTarget _target;
        private Browser _browser;
        private BrowserHost _browserHost;

        public Viewport(IRenderTarget target, BrowserDelegate browserDelegate) {
            _target = target;
            _target.TargetClosing += OnTargetClosing;
            _target.TargetClosed += OnTargetClosed;
            _target.TargetSizeChanged += OnTargetSizeChanged;

            _handler = new ClientHandler(browserDelegate);

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
            base.DisposeManaged();
        }

        protected override void DisposeNative() {
            _handler.Dispose();
            _browser.Dispose();
            _browserHost.Dispose();
            base.DisposeNative();
        }

        private void OnTargetClosing(object sender, EventArgs e) {
            _browserHost.ParentWindowWillClose();
        }

        private void OnTargetClosed(object sender, EventArgs e) {
            // nada
        }

        private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) {
            var bounds = new Rectangle(0, 0, e.Size.Width, e.Size.Height);
            _resizer.Resize(_browserHost.WindowHandle, bounds);
        }

        internal void CreateBrowser() {
            OnCreating(EventArgs.Empty);

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

            // Starts the browser rendering loop.
            _browser = BrowserHost.CreateBrowser(a);
            _browserHost = _browser.Host;

            OnCreated(EventArgs.Empty);

            _target.Show();
        }
    }
}
