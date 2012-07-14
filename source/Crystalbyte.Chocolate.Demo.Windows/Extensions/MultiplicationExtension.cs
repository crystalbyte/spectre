using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.Extensions
{
    internal sealed class MultiplicationExtension : ScriptingExtension
    {
        public override string PrototypeCode
        {
            get { return "native function mult(a, b);"; }
        }

        protected override void OnExecuted(ExecutedEventArgs e)
        {
            var a = e.Arguments[0].ToInteger();
            var b = e.Arguments[1].ToInteger();
            e.Result = new ScriptableObject(a * b);
            e.IsHandled = true;
        }
    }
}
