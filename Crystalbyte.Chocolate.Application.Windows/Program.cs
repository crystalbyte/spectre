#region Namespace Directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;
using Crystalbyte.Chocolate.UI;
using System.IO;

#endregion

namespace Crystalbyte.Chocolate {
    internal static class Program {

        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Framework.Settings.IsSingleProcess = true;
            var a = new AppDelegate();
            a.BrowserCreated += OnBrowserCreated;
            a.IpcMessageReceived += OnIpcMessageReceived;
            a.ProcessStarted += OnProcessStarted;
            var module = Assembly.GetExecutingAssembly().ManifestModule;
            var success = Framework.Initialize(module, a);
            if (!success) {
                Debug.WriteLine("initialization failed");
                return;
            }

            if (!Framework.IsRootProcess) {
                return;
            }

            var b = new MyBrowserDelegate();
            b.PageLoaded += OnPageLoaded;
            var index = new Uri("http://www.graphicscardbenchmarks.com/page/webgl");
            var renderer = new HtmlRenderer(new Window {StartupUri = index}, b);
            Framework.Run(renderer);
            Framework.Shutdown();
        }

        private static void OnPageLoaded(object sender, PageLoadedEventArgs e) {
            Debug.WriteLine(e.Frame.Url);
            const string text = "This message was sent across process boundaries.";
            var bytes = Encoding.UTF8.GetBytes(text);
            var ms = new MemoryStream(bytes);
            e.Browser.SendIpcMessage(ProcessType.Renderer, new IpcMessage("Hello") {
                Payload = ms
            });
        }

        private static void OnProcessStarted(object sender, ProcessStartedEventArgs e) {
            
        }

        private static void OnIpcMessageReceived(object sender, IpcMessageReceivedEventArgs e) {
            using (var sr = new StreamReader(e.Message.Payload)) {
                var text = sr.ReadToEnd();
                
            }
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