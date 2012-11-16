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

namespace Crystalbyte.Spectre.Web {
    public sealed class CallbackObject : RefCountedNativeTypeAdapter {
        private CallbackObject(IntPtr handle)
            : base(typeof (CefCallback)) {
            Handle = handle;
        }

        public bool IsPaused { get; private set; }

        public static CallbackObject FromHandle(IntPtr handle) {
            return new CallbackObject(handle);
        }

        public void Resume() {
            var r = MarshalFromNative<CefCallback>();
            var action = (CefCallbackCapiDelegates.ContCallback2)
                         Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (CefCallbackCapiDelegates.ContCallback2));
            action(Handle);
        }

        public void Pause() {
            IsPaused = true;
        }
    }
}
