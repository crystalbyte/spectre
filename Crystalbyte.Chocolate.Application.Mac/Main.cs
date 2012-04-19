using System;
using Gtk;
using Crystalbyte.Chocolate.UI;
using System.Diagnostics;

namespace Crystalbyte.Chocolate.Application.Mac
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Gtk.Application.Init ();
			
			var success = Framework.Initialize(args, new AppDelegate());
			if (!success) {
				Debug.WriteLine("Initialization failed.");
				return;
			}
			if (!Framework.IsRootProcess) {
				return;
			}
			
			var startupUri = new Uri("http://www.battleshipmovie.com/#/home"); 
			var renderer =new HtmlRenderer(new MainWindow { StartupUri = startupUri}, new BrowserDelegate());
			Framework.Run(renderer);
			Framework.Shutdown();
		}
	}
}
