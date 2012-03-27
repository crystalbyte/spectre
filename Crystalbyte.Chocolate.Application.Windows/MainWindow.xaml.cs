using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Crystalbyte.Chocolate.UI;
using Application = Crystalbyte.Chocolate.UI.Application;
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
            Loaded += OnWindowLoaded;
            _interopHelper = new WindowInteropHelper(this);
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e) {
            _interopHelper.EnsureHandle();
            Application.Register(new View(this, new WindowDelegate()));
            Dispatcher.Hooks.DispatcherInactive += OnDispatcherInactive;
            Dispatcher.Hooks.OperationCompleted += OnOperationCompleted;
        }

        private void OnOperationCompleted(object sender, DispatcherHookEventArgs e) {
            Application.IterateMessageLoop();
        }

        private void OnDispatcherInactive(object sender, EventArgs e) {
            Application.IterateMessageLoop();
        }

        public IntPtr Handle {
            get { return _interopHelper.Handle; }
        }

        public Uri StartupUri {
            get { return new Uri("http://www.google.de"); }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            NotifySizeChanged(GetActualSize());
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnClosed(EventArgs e) {
            NotifyClosed();
            base.OnClosed(e);
        }

        private Size GetActualSize() {
            return new Size((int) ActualWidth, (int) ActualHeight);
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

        public Size StartSize
        {
            get { return GetActualSize(); }
        }
    }
}
