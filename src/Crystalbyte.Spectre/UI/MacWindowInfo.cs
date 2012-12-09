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
    public sealed class MacWindowInfo : CefTypeAdapter {
        private readonly bool _isOwned;

        public MacWindowInfo(IRenderTarget target)
            : base(typeof (MacCefWindowInfo)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new MacCefWindowInfo {
                ParentView = target.Handle,
                X = 0,
                Y = 0,
                Width = target.Size.Width,
                Height = target.Size.Height
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
