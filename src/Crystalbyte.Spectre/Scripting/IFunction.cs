using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Scripting {
    public interface IFunction {
        string Name { get;}
        JavaScriptHandler Handler { get; }
        JavaScriptObject Execute(params JavaScriptObject[] arguments);
        JavaScriptObject Execute(JavaScriptObject target, params JavaScriptObject[] arguments);
        JavaScriptObject Execute(ScriptingContext context, JavaScriptObject target, params JavaScriptObject[] arguments);
    }
}
