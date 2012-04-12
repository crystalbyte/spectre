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
    // use this only for debugging purposes
            Framework.Settings.IsSingleProcess = true;
#endif
            var appDelegate = new AppDelegate();
            appDelegate.Initialized += OnInitialized;
            appDelegate.BrowserCreated += OnBrowserCreated;
            appDelegate.ContextReleased += OnContextCreated;
            var module = Assembly.GetExecutingAssembly().ManifestModule;

            var success = Framework.Initialize(module, appDelegate);
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

        private static void OnContextCreated(object sender, ContextEventArgs e) {
            Debug.WriteLine(e.Context.Document.Length);
        }

        private static void OnBrowserCreated(object sender, BrowserEventArgs e) {
            //foreach (var frame in e.Browser.Frames) {
            //    Debug.WriteLine(frame.Name);
            //}

            e.Browser.MainFrame.Navigate("http://www.google.com");
            var document = e.Browser.MainFrame.Context.Document;

            Debug.Assert(e.Browser != null);
            Debug.Assert(e.Browser.FocusedFrame != null);
            Debug.Assert(e.Browser.MainFrame != null);
        }
      
        private static void OnInitialized(object sender, EventArgs e) {
            ScriptingRuntime.RegisterExtension("myExtension", new TestHandler());
        }
    }
}