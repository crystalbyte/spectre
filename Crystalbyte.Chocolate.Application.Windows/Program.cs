#region Namespace Directives

using System;
using System.Diagnostics;
using System.Reflection;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    internal static class Program {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            var module = Assembly.GetExecutingAssembly().ManifestModule;
            var success = Framework.Initialize(module, new AppDelegate());
            if (!success) {
                Debug.WriteLine("initialization failed");
                return;
            }

            if (!Framework.IsRootProcess) {
                return;
            }

            var index = new Uri("http://www.battleshipmovie.com/#/home");
            var renderer = new HtmlRenderer(new Window {StartupUri = index}, new BrowserDelegate());
            Framework.Run(renderer);
            Framework.Shutdown();
        }
    }
}