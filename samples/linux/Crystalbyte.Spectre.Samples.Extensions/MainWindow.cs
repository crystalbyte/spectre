using System;
using Gtk;
using Crystalbyte.Spectre.UI;

public partial class MainWindow: Gtk.Window, IRenderTarget
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		OnTargetClosing(EventArgs.Empty);
		OnTargetClosed(EventArgs.Empty);
	}

	protected override bool OnConfigureEvent (Gdk.EventConfigure evnt) {
		OnTargetSizeChanged(new SizeChangedEventArgs(Size));
		return base.OnConfigureEvent (evnt);
	}

	#region IRenderTarget implementation
	public event EventHandler<SizeChangedEventArgs> TargetSizeChanged;

	protected virtual void OnTargetSizeChanged (SizeChangedEventArgs e)
	{
		var handler = this.TargetSizeChanged;
		if (handler != null)
			handler (this, e);
	}
	public event EventHandler TargetClosing;

	protected virtual void OnTargetClosing (EventArgs e)
	{
		var handler = this.TargetClosing;
		if (handler != null)
			handler (this, e);
	}
	public event EventHandler TargetClosed;

	protected virtual void OnTargetClosed (EventArgs e)
	{
		var handler = this.TargetClosed;
		if (handler != null)
			handler (this, e);
	}

	public Size Size {
		get {
			int width;
			int height;
			this.GetSize(out width, out height);
			return new Size(width, height);
		}
	}

	public Uri StartupUri {
		get; set;
	}
	#endregion

}
