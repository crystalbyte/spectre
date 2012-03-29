using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crystalbyte.Chocolate.UI;
using Size = Crystalbyte.Chocolate.UI.Size;

namespace Crystalbyte.Chocolate
{
    public partial class Window : Form, IRenderTarget
    {
        public Window() {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e){
            NotifyTargetClosing();
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            NotifySizeClosed();            
            base.OnClosed(e);
        }

        public Uri StartupUri {
            get { return new Uri("http://peacekeeper.futuremark.com/"); }
        }

        protected override void OnSizeChanged(EventArgs e) {
            NotifySizeChanged(GetActualSize());
            base.OnSizeChanged(e);
        }

        private Size GetActualSize() {
            return new Size(Width, Height);
        }

        public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
        public void NotifySizeChanged(Size size) {
            var handler = TargetSizeChanged;
            if (handler != null) {
                handler(this, new SizeChangedEventArgs(size));
            }
        }

        public event EventHandler TargetClosed;
        public void NotifySizeClosed() {
            var handler = TargetClosed;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        public Size StartSize {
            get { return GetActualSize(); }
        }

        public event EventHandler TargetClosing;
        public void NotifyTargetClosing()
        {
            var handler = TargetClosing;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
