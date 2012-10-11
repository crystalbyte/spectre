#region Using directives

using System;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class CommandLine : RefCountedNativeObject{
        public CommandLine()
            : base(typeof (CefCommandLine)){
            NativeHandle = CefCommandLineCapi.CefCommandLineCreate();
        }

        private CommandLine(IntPtr handle)
            : base(typeof (CefCommandLine)){
            NativeHandle = handle;
        }

        public static CommandLine Current{
            get{
                var handle = CefCommandLineCapi.CefCommandLineGetGlobal();
                return FromHandle(handle);
            }
        }

        public static CommandLine FromHandle(IntPtr handle){
            return new CommandLine(handle);
        }
    }
}