using System;
using Gtk;

namespace Crystalbyte.Spectre.Samples.Linux.Video
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var strapper = new GtkBootstrapper();
			strapper.Run();
		}
	}
}
