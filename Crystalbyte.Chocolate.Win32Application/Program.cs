#region Namespace Directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;
using Crystalbyte.Chocolate.UI;
using System.Threading;

#endregion

namespace Crystalbyte.Chocolate {
    internal static class Program {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            var appDelegate = new AppDelegate();
            appDelegate.Initialized += OnInitialized;
            var module = Assembly.GetExecutingAssembly().ManifestModule;

            var success = Framework.Initialize(module, appDelegate);
            if (!success) {
                Debug.WriteLine("initialization failed");
                return;
            }

            if (!Framework.IsRootProcess) {
                return;
            }

            //var index = new Uri("http://www.battleshipmovie.com/");
            var index = new Uri(Environment.CurrentDirectory + "/Pages/start.htm");
            var process = new RenderProcess(new Window {StartupUri = index}, new BrowserDelegate());
            Framework.Run(process);
            Framework.Shutdown();
        }

        private static void OnInitialized(object sender, EventArgs e) {
            ScriptingRuntime.RegisterExtension("myExtension", new TestHandler());
        }
    }
}