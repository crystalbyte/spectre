using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.Extensions
{
    internal sealed class MandelbrotExtension : ScriptingExtension
    {
        public override string PrototypeCode
        {
            get { return "native function computMandelbrot();"; }
        }

        protected override void OnExecuted(ExecutedEventArgs e)
        {
            MessageBox.Show("Mandelbrot called;");
        }
    }
}
