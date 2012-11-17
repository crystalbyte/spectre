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
