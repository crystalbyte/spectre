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

		protected override void InitializeRenderProcess ()
		{
			Application.Init ();
		}

		protected override void ConfigureSettings (FrameworkSettings settings)
		{
			var modulePath = new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;

			// redirect sub process to the mono runtime
			var commandLine = Environment.GetCommandLineArgs();
			settings.BrowserSubprocessPath = string.Format("/usr/bin/mono \"{0}\"", commandLine[0]);

			// set the locale and resource locations, for they are not next to the mono executable.
			settings.Locale = "de-DE";
			settings.LocalesDirPath = Path.Combine(modulePath, "locales");
			settings.PackFilePath = Path.Combine(modulePath);
		}
	}
}

