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

namespace Crystalbyte.Spectre.Scripting {
    public sealed class ScriptingExceptionObject : CefTypeAdapter {
        public ScriptingExceptionObject()
            : base(typeof (CefV8exception)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
        }

        public string Message {
            get {
                var r = MarshalFromNative<CefV8exception>();
                var function = (CefV8CapiDelegates.GetMessageCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMessage,
                                                                     typeof (CefV8CapiDelegates.GetMessageCallback));
                var handle = function(Handle);
                return StringUtf16.ReadString(handle);
            }
        }

        protected override void DisposeNative() {
            if (Handle != IntPtr.Zero) {
                Marshal.FreeHGlobal(Handle);
            }
            base.DisposeNative();
        }
    }
}
