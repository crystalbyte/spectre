using System;
using Gtk;
using Crystalbyte.Chocolate.UI;

public partial class MainWindow: Gtk.Window, IRenderTarget
{	
	public MainWindow ()
		: base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Resize(1600, 900);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		NotifyTargetClosing();
		NotifyTargetClosed();
		a.RetVal = true;
	}

	protected override void OnSizeAllocated (Gdk.Rectangle allocation) {
		base.OnSizeAllocated (allocation);
		NotifyTargetSizeChanged(new SizeChangedEventArgs(Size));
	}

	#region IRenderTarget implementation

	public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
	private void NotifyTargetSizeChanged (SizeChangedEventArgs e) {
		var handler = TargetSizeChanged;
		if (handler != null) {
			handler(this, e);
		}
	}

	public event EventHandler TargetClosing;
	private void NotifyTargetClosing () {
		var handler = TargetClosing;
		if (handler != null) {
			handler(this, EventArgs.Empty);
		}
	}

	public event EventHandler TargetClosed;
	private void NotifyTargetClosed () {
		var handler = TargetClosed;
		if (handler != null) {
			handler(this, EventArgs.Empty);
		}
	}

	public Size Size {
		get {
			return new Size(Screen.Width, Screen.Height);
		}
	}

	public Uri StartupUri {
		get; set;
	}

	#endregion

}
