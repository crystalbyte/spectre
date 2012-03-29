using System;
using Crystalbyte.Chocolate.UI;
using ChocApp = Crystalbyte.Chocolate.UI.Application;
using WinApp = System.Windows.Forms.Application;

namespace Crystalbyte.Chocolate
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var window = new Window();
            var view = new View(window, new WindowDelegate());
            ChocApp.Register(view);
            window.Show();
            ChocApp.RunMessageLoop();
        }
    }
}
