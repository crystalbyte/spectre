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
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class WindowsWindowInfo : CefTypeAdapter {
        private readonly bool _isOwned;

        public WindowsWindowInfo(IRenderTarget target)
            : base(typeof (WindowsCefWindowInfo)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new WindowsCefWindowInfo {
                ParentWindow = target.Handle,
                Style = (uint) (WindowStyles.ChildWindow
                                | WindowStyles.ClipChildren
                                | WindowStyles.ClipSiblings
                                | WindowStyles.TabStop
                                | WindowStyles.Visible),
                X = 0,
                Y = 0,
                Width = target.Size.Width,
                Height = target.Size.Height
            });
            _isOwned = true;
        }

        private WindowsWindowInfo(IntPtr handle)
            : base(typeof (WindowsCefWindowInfo)) {
            Handle = handle;
        }

        public IntPtr WindowHandle {
            get {
                var reflection = MarshalFromNative<WindowsCefWindowInfo>();
                return reflection.Window;
            }
        }

        public static WindowsWindowInfo FromHandle(IntPtr handle) {
            return new WindowsWindowInfo(handle);
        }

        protected override void DisposeNative() {
            if (Handle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(Handle);
            }
            base.DisposeNative();
        }
    }
}
