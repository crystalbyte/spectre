using System;
using Gtk;
using Crystalbyte.Chocolate.UI;

public partial class MainWindow: Gtk.Window, IRenderTarget
{	
	public MainWindow () 
		: base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	#region IRenderTarget implementation
	public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
	private void NotifySizeChanged(Size size) {
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

	public Size Size {
		get {
			int width;
			int height;
			GetSize(out width, out height);
			return new Size(width, height);
		}
	}

	public Uri StartupUri {
		get; set;
	}
	#endregion
}
