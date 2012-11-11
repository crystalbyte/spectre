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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Threading {
    internal sealed class Task : OwnedRefCountedNativeObject {
        private readonly Action _action;
        private readonly CefTaskCapiDelegates.ExecuteCallback _executeCallback;

        public event EventHandler Executed;

        private void OnExecuted(EventArgs e) {
            var handler = Executed;
            if (handler != null)
                handler(this, e);
        }

        public Task(Action action)
            : base(typeof (CefTask)) {
            _action = action;
            _executeCallback = OnExecute;
            MarshalToNative(new CefTask {
                Base = DedicatedBase,
                Execute = Marshal.GetFunctionPointerForDelegate(_executeCallback)
            });
        }

        private void OnExecute(IntPtr self, CefThreadId threadid) {
            if (_action != null) {
                _action();
            }
            OnExecuted(EventArgs.Empty);
        }
    }
}
