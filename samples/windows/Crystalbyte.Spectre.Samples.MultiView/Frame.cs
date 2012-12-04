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
    public partial class Frame : UserControl, IRenderTarget {
        public Frame() {
            InitializeComponent();
        }

        #region IRenderTarget Members

        public Uri StartupUri {
            get { return new Uri("spectre://localhost/Views/index.html"); }
        }

        public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;

        public void NotifySizeChanged() {
            var handler = TargetSizeChanged;
            if (handler == null) return;
            var size = ((IRenderTarget) this).Size;
            handler(this, new SizeChangedEventArgs(size));
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

        Size IRenderTarget.Size {
            get { return new Size(ClientRectangle.Width, ClientRectangle.Height); }
        }

        #endregion

        public void OnClosing(CancelEventArgs e) {
            NotifyTargetClosing();
        }

        public void OnClosed(EventArgs e) {
            NotifyTargetClosed();
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            NotifySizeChanged();
        }
    }
}
