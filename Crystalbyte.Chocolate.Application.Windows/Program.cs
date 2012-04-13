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
            var module = Assembly.GetExecutingAssembly().ManifestModule;
            var success = Framework.Initialize(module, new AppDelegate());
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
    }
}