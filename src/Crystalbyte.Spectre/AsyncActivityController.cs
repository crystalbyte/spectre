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

#endregion

namespace Crystalbyte.Spectre {
    public sealed class AsyncActivityController : RefCountedCefTypeAdapter {
        private AsyncActivityController(IntPtr handle)
            : base(typeof (CefCallback)) {
            Handle = handle;
        }

        public bool IsCanceled { get; private set; }
        public bool IsPaused { get; private set; }

        public static AsyncActivityController FromHandle(IntPtr handle) {
            return new AsyncActivityController(handle);
        }

        public void Continue() {
            var r = MarshalFromNative<CefCallback>();
            var action =
                (CefCallbackCapiDelegates.ContCallback2)
                Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (CefCallbackCapiDelegates.ContCallback2));
            action(Handle);
        }

        public void Cancel() {
            var r = MarshalFromNative<CefCallback>();
            var action =
                (CefCallbackCapiDelegates.CancelCallback)
                Marshal.GetDelegateForFunctionPointer(r.Cancel, typeof (CefCallbackCapiDelegates.CancelCallback));
            action(Handle);
            IsCanceled = true;
        }

        public void Pause() {
            IsPaused = true;
        }
    }
}
