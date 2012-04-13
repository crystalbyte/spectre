#region Namespace Directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Crystalbyte.Chocolate.Scripting;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    internal static class Program {

        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
#if (DEBUG)
            // use this only for debugging purposes, sp mode is not yet as stable as it should be
            Framework.Settings.IsSingleProcess = true;
#endif
            var a = new AppDelegate();
            a.BrowserCreated += OnBrowserCreated;
            var module = Assembly.GetExecutingAssembly().ManifestModule;
            var success = Framework.Initialize(module, a);
            if (!success) {
                Debug.WriteLine("initialization failed");
                return;
            }

            if (!Framework.IsRootProcess) {
                return;
            }

            var index = new Uri("http://www.battleshipmovie.com/");
            var process = new RenderProcess(new Window {StartupUri = index}, new BrowserDelegate());
            Framework.Run(process);
            Framework.Shutdown();
        }

        private static void OnBrowserCreated(object sender, BrowserEventArgs e) {
            var names = e.Browser.FrameNames;
            foreach (var name in names) {
                Debug.WriteLine(name);
            }
        }
    }
}