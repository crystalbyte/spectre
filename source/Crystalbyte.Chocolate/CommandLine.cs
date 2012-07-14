#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class CommandLine : Adapter {
        public CommandLine()
            : base(typeof (CefCommandLine), true) {
            NativeHandle = CefCommandLineCapi.CefCommandLineCreate();
        }

        private CommandLine(IntPtr handle)
            : base(typeof (CefCommandLine), true) {
            NativeHandle = handle;
        }

        public static CommandLine Current {
            get {
                var handle = CefCommandLineCapi.CefCommandLineGetGlobal();
                return FromHandle(handle);
            }
        }

        public static CommandLine FromHandle(IntPtr handle) {
            return new CommandLine(handle);
        }
    }
}