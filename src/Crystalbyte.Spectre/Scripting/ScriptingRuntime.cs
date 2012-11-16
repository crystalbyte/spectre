#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
