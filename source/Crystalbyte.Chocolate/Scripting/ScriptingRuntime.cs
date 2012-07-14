#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public static class ScriptingRuntime {
        public static bool RegisterExtension(string name, ScriptingExtension extension) {
            var n = new StringUtf16(name);
            var j = new StringUtf16(extension.PrototypeCode);
            var result = CefV8Capi.CefRegisterExtension(n.NativeHandle, j.NativeHandle, extension.NativeHandle);
            return Convert.ToBoolean(result);
        }
    }
}