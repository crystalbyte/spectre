using System;
using Crystalbyte.Chocolate;
using Crystalbyte.Chocolate.UI;
using Gtk;
using System.Reflection;
using System.IO;

namespace Crystalbyte.Chocolate.Demo.Linux
{
	public sealed class GtkBootstrapper : Bootstrapper
	{
		protected override IRenderTarget CreateRenderTarget ()
		{
			return new MainWindow { StartupUri =  new Uri("http://trailers.apple.com/trailers/fox/prometheus/") };
		}

		protected override void InitializeRenderer ()
		{
			Application.Init ();
		}

		protected override void InitializeFramework ()
		{
			var modulePath = Assembly.GetEntryAssembly().Location;

			// redirect sub process to the mono runtime
			var commandLine = Environment.GetCommandLineArgs();
			Framework.Settings.BrowserSubprocessPath = string.Format("/usr/bin/mono \"{0}\"", commandLine[0]);

			// set the locale and resource locations, for they are not next to the mono executable.
			Framework.Settings.LocalesDirPath = Path.Combine(modulePath, "locales");
			Framework.Settings.PackFilePath = modulePath;
		}
	}
}

