using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Application
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;
		
		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
			
			var window = mainWindowController.Window;
			Framework.Add(new HtmlRenderer(window, new BrowserDelegate()));
		}
	}
}

