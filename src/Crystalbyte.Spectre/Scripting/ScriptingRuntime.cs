#region Using directives

using System;
using System.Collections.Generic;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Scripting{
    public static class ScriptingRuntime{

        private static readonly List<ScriptingCommand> _commands = new List<ScriptingCommand>();
        public static List<ScriptingCommand> Commands { get { return _commands; } }

        public static bool RegisterCommand(string name, ScriptingCommand command){

            // keep strong reference
            Commands.Add(command);

            var n = new StringUtf16(name);
            var j = new StringUtf16(command.RegistrationCode);
            var result = CefV8Capi.CefRegisterExtension(n.NativeHandle, j.NativeHandle, command.NativeHandle);
            return Convert.ToBoolean(result);
        }
    }
}