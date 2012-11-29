#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
