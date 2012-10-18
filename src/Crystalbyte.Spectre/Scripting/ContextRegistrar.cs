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

using System.Collections.Generic;
using System.Linq;
using Crystalbyte.Spectre.Threading;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public sealed class ContextRegistrar {
        private readonly List<ScriptingContext> _contextList;
        private static readonly ContextRegistrar _current = new ContextRegistrar();

        private ContextRegistrar() {
            _contextList = new List<ScriptingContext>();
            if (Application.Current != null) {
                // This is necessary to allow the GC to collect all destroyed context objects
                Application.Current.ShutdownStarted += (sender, e) => _contextList.Clear();
            }
        }

        public static ContextRegistrar Current {
            get { return _current; }
        }

        public void Register(ScriptingContext context) {
            VerifyAccess();
            _contextList.Add(context);
        }

        public bool IsContextAlive(ScriptingContext context) {
            VerifyAccess();
            return _contextList.Any(x => x.IsSame(context));
        }

        public bool Remove(ScriptingContext context) {
            VerifyAccess();
            return _contextList.Remove(context);
        }

        public void VerifyAccess() {
            if (!Dispatcher.Current.IsCurrentlyOn(DispatcherQueue.Renderer)) {
                Errors.ThrowInvalidCrossThreadCall(DispatcherQueue.Renderer);
            }
        }
    }
}
