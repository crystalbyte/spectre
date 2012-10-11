using System;

namespace Crystalbyte.Spectre.Samples {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var bootstrapper = new WinformsBootstrapper();
            bootstrapper.Run();
        }
    }
}
