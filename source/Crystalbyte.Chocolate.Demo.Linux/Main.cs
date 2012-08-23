using System;
using Gtk;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Demo.Linux
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var strapper = new GtkBootstrapper ();
			strapper.Run ();
		}
	}
}
