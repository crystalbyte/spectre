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
    public sealed class LinuxWindowInfo : NativeTypeAdapter {
        private readonly bool _isOwned;

        public LinuxWindowInfo(IRenderTarget target)
            : base(typeof (LinuxCefWindowInfo)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new LinuxCefWindowInfo {
                ParentWidget = target.Handle
            });
            _isOwned = true;
        }

        protected override void DisposeNative() {
            if (Handle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(Handle);
            }
            base.DisposeNative();
        }
    }
}
