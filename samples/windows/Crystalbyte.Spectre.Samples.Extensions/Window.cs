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
using System.ComponentModel;
using System.Windows.Forms;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public partial class Window : Form, IRenderTarget {
        public Window() {
            InitializeComponent();
        }

        #region IRenderTarget Members

        public Uri StartupUri { get; set; }

        public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;

        public void NotifySizeChanged(Size size) {
            var handler = TargetSizeChanged;
            if (handler != null) {
                handler(this, new SizeChangedEventArgs(size));
            }
        }

        public event EventHandler TargetClosed;

        public void NotifyTargetClosed() {
            var handler = TargetClosed;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler TargetClosing;

        public void NotifyTargetClosing() {
            var handler = TargetClosing;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        public new Size Size {
            get { return new Size(ClientRectangle.Width, ClientRectangle.Height); }
        }

        #endregion

        protected override void OnClosing(CancelEventArgs e) {
            NotifyTargetClosing();
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            NotifyTargetClosed();
            base.OnClosed(e);
        }

        protected override void OnSizeChanged(EventArgs e) {
            NotifySizeChanged(Size);
            base.OnSizeChanged(e);
        }
    }
}
