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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Threading {
    internal sealed class Task : OwnedRefCountedCefTypeAdapter {
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
