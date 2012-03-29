using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Crystalbyte.Chocolate.UI;
using Size = Crystalbyte.Chocolate.UI.Size;
using Application = Crystalbyte.Chocolate.UI.Application;
using SizeChangedEventArgs = Crystalbyte.Chocolate.UI.SizeChangedEventArgs;

namespace orgAnice.Chocolate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IRenderTarget {
        private readonly DispatcherTimer _timer;
        private readonly WindowInteropHelper _interopHelper;

        public MainWindow() {
            InitializeComponent();
            Loaded += OnWindowLoaded;
            _interopHelper = new WindowInteropHelper(this);
            // 60 FPS
            var interval = TimeSpan.FromMilliseconds(1000.0f / 60.0f);
            const DispatcherPriority priority = DispatcherPriority.Normal;
            _timer = new DispatcherTimer(interval, priority, OnTimerElapsed, Dispatcher.CurrentDispatcher);
        }

        private static void OnTimerElapsed(object sender, EventArgs e) {
            Application.IterateMessageLoop();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e) {
            Application.Register(new View(this, new WindowDelegate()));
            _timer.Start();
        }

        protected override void OnClosing(CancelEventArgs e){
            NotifyTargetClosing();
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            NotifySizeClosed();            
            base.OnClosed(e);
        }

        public IntPtr Handle {
            get { return _interopHelper.Handle; }
        }

        public Uri StartupUri {
            get { return new Uri("http://peacekeeper.futuremark.com/"); }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            NotifySizeChanged(GetActualSize());
            base.OnRenderSizeChanged(sizeInfo);
        }

        private Size GetActualSize() {
            return new Size((int) ActualWidth, (int) ActualHeight);
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
