using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crystalbyte.Spectre.UI;
using Size = Crystalbyte.Spectre.UI.Size;

namespace Crystalbyte.Spectre.Samples {
    public partial class Frame : UserControl, IRenderTarget {

        public Frame() {
            InitializeComponent();
        }

        #region IRenderTarget Members

        public Uri StartupUri { get { return new Uri("spectre://localhost/Views/index.html"); } }

        public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;

        public void NotifySizeChanged() {
            var handler = TargetSizeChanged;
            if (handler != null) {
                var size = ((IRenderTarget) this).Size;
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
