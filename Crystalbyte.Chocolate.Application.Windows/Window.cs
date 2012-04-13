#region Namespace Directives

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public partial class Window : Form, IRenderTarget {
        public Window() {
            InitializeComponent();
        }

        #region IRenderTarget Members

        public Uri StartupUri { get; set; }

        public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
        public event EventHandler TargetClosed;
        public event EventHandler TargetClosing;

        public new Size Size {
            get { return new Size(base.Size.Width, base.Size.Height); }
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

        public void NotifySizeChanged(Size size) {
            var handler = TargetSizeChanged;
            if (handler != null) {
                handler(this, new SizeChangedEventArgs(size));
            }
        }

        public void NotifyTargetClosed() {
            var handler = TargetClosed;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        public void NotifyTargetClosing() {
            var handler = TargetClosing;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }
    }
}