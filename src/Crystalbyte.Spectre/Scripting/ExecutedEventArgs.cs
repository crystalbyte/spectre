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

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public sealed class ExecutedEventArgs : EventArgs {
        public string FunctionName { get; internal set; }
        public IReadOnlyCollection<JavaScriptObject> Arguments { get; internal set; }
        public JavaScriptObject Result { get; set; }
        public JavaScriptObject Target { get; internal set; }
        public bool IsHandled { get; set; }
        public Exception Exception { get; set; }
    }
}
