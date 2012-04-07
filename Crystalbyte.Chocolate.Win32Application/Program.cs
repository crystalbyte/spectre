#region Namespace Directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
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
            var module = Assembly.GetExecutingAssembly().ManifestModule;
            var success = Framework.Initialize(module);
            if (!success) {
                Debug.WriteLine("initialization failed");
                return;
            }

            if (!Framework.IsRootProcess) {
                return;
            }

            //var index = new Uri("http://www.google.com/");
            var index = new Uri(Environment.CurrentDirectory + "/Pages/start.htm");
            var process = new RenderProcess(new Window { StartupUri = index }, new WindowDelegate());
            Framework.Run(process);
            Framework.Shutdown();
        }
    }
}