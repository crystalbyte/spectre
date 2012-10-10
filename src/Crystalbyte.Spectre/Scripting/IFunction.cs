using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Scripting {
    public interface IFunction {
        string Name { get;}
        JavaScriptHandler Handler { get; }
        JavaScriptObject Execute(JavaScriptObject scope, params JavaScriptObject[] arguments);
        JavaScriptObject Execute(JavaScriptContext context, params JavaScriptObject[] arguments);
    }
}
