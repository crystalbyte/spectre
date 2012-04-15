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

#if DEBUG
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

            var b = new BrowserDelegate();
            var index = new Uri("http://www.google.com");
            var process = new RenderProcess(new Window {StartupUri = index}, b);
            Framework.Run(process);
            Framework.Shutdown();
        }

        private static void OnBrowserCreated(object sender, BrowserEventArgs e) {
            var names = e.Browser.FrameNames;
            var keys = e.Browser.MainFrame.Context.Document.Keys;
            foreach (var name in names) {
                Debug.WriteLine(name);
            }
        }
    }
}