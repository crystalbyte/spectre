#region Namespace Directives

using System;
using System.Diagnostics;
using System.Reflection;
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
            Framework.Settings.IsSingleProcess = true;
#endif 
            var appDelegate = new AppDelegate();
            appDelegate.Initialized += OnInitialized;
            appDelegate.ScriptingContextCreated += OnScriptingContextCreated;
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

        private static void OnScriptingContextCreated(object sender, ContextEventArgs e) {
            
        }

        private static void OnInitialized(object sender, EventArgs e) {
            ScriptingRuntime.RegisterExtension("myExtension", new TestHandler());
        }
    }
}