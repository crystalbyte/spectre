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

using System.Linq;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class IncScriptingCommand : ScriptingCommand {
        public override string RegistrationCode {
            get {
                // The following code will synthesize the function accessor.
                // window.commands.increment = function(value) { ... };
                return RegistrationCodes.Synthesize(
                    "commands", // container object
                    "increment", // Function name
                    "value"); // Argument list
            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            if (e.Arguments.Count < 1) {
                e.Result = new JavaScriptObject(1);
            }
            else {
                var b = e.Arguments.First().ToInteger();
                e.Result = new JavaScriptObject(b + 1);
            }

            e.IsHandled = true;
        }
    }
}
