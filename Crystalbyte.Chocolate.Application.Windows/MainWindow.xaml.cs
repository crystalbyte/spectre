using System;
using System.Windows;
using System.Windows.Interop;
using Crystalbyte.Chocolate.UI;
using Size = Crystalbyte.Chocolate.UI.Size;
using SizeChangedEventArgs = Crystalbyte.Chocolate.UI.SizeChangedEventArgs;

namespace orgAnice.Chocolate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IRenderTarget {
        private readonly WindowInteropHelper _interopHelper;
        public MainWindow() {
            InitializeComponent();
            _interopHelper = new WindowInteropHelper(this);
        }

        public IntPtr Handle {
            get { return _interopHelper.Handle; }
        }

        public Uri StartupUri {
            get { return new Uri("http://www.google.de"); }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            NotifySizeChanged(new Size((int) ActualWidth, (int) ActualHeight));
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnClosed(EventArgs e) {
            NotifyClosed();
            base.OnClosed(e);
        }

        public new event EventHandler<SizeChangedEventArgs> SizeChanged;
        public void NotifySizeChanged(Size size) {
            var handler = SizeChanged;
            if (handler != null) {
                handler(this, new SizeChangedEventArgs(size));
            }
        }

        public new event EventHandler Closed;
        public void NotifyClosed() {
            var handler = Closed;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            var context = (WindowModelView) DataContext;
            context.Run();
        }
    }
}
