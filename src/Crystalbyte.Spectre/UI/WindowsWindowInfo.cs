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
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class WindowsWindowInfo : NativeTypeAdapter {
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
