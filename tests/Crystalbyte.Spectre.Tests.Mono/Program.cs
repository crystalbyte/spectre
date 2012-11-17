using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Crystalbyte.Spectre.Tests.Mono {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var strapper = new WinformsBootstrapper();
            strapper.Run();
        }
    }
}
