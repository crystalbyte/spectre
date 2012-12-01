#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public static class ScriptingRuntime {
        private static readonly List<ScriptingCommand> _commands = new List<ScriptingCommand>();

        public static IEnumerable<ScriptingCommand> RegisteredCommands {
            get { return _commands; }
        }

        public static bool RegisterCommand(string name, ScriptingCommand command) {
            // keep strong reference
            _commands.Add(command);

            var n = new StringUtf16(name);
            var j = new StringUtf16(command.RegistrationCode);
            var result = CefV8Capi.CefRegisterExtension(n.Handle, j.Handle, command.Handle);
            return Convert.ToBoolean(result);
        }
    }
}
