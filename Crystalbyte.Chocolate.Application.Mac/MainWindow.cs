using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Application
{
	public partial class MainWindow : NSWindow, IRenderTarget
	{
		#region Constructors
		
		// Called when created from unmanaged code
		public MainWindow (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindow (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			WindowShouldClose = OnWindowShouldClose;
			WillClose += OnWillClose;
		}
		
		#endregion
		
		private bool OnWindowShouldClose(NSObject sender) {
			var shouldClose = true;
			if (shouldClose) {
				NotifyTargetClosing();	
			}
			return shouldClose;
		}
		
		private void OnWillClose(object sender, EventArgs e) {
			NotifyTargetClosed();
		}
		
		public override void AwakeFromNib () {
			var frame = NSScreen.MainScreen.Frame;
			frame.Width = 1280;
			frame.Height = 768;
			SetFrame(frame, false, true);
			Center();
			base.AwakeFromNib ();
		}

		#region IRenderTarget implementation
		public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
		private void NotifyTargetSizeChanged(Size size) {
			var handler = TargetSizeChanged;
			if (handler != null) {
				handler(this, new SizeChangedEventArgs(size));
			}
		}

		public event EventHandler TargetClosing;
		private void NotifyTargetClosing() {
			var handler = TargetClosing;
			if (handler != null) {
				handler(this, EventArgs.Empty);
			}
		}

		public event EventHandler TargetClosed;
		private void NotifyTargetClosed() {
			var handler = TargetClosed;
			if (handler != null) {
				handler(this, EventArgs.Empty);
			}
		}

		public void Show () {
			this.Display();
		}
		
		public new IntPtr Handle {
			get {
				return ContentView.Handle;
			}
		}

		public Size Size {
			get {
				return new Size((int)ContentView.Frame.Width, (int)ContentView.Frame.Height);
			}
		}

		public Uri StartupUri {
			get {
				return new Uri("http://www.battleshipmovie.com/#/home", UriKind.Absolute);
			}
		}
		#endregion
	}
}

