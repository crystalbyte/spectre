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
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class SchemeRegistrar : OwnedRefCountedNativeObject {
        private SchemeRegistrar(IntPtr handle)
            : base(typeof (CefSchemeRegistrar)) {
            NativeHandle = handle;
        }

        public static SchemeRegistrar FromHandle(IntPtr handle) {
            return new SchemeRegistrar(handle);
        }

        public void Register(ISchemeDescriptor descriptor) {
            var r = MarshalFromNative<CefSchemeRegistrar>();
            var function = (CefSchemeCapiDelegates.AddCustomSchemeCallback)
                           Marshal.GetDelegateForFunctionPointer(r.AddCustomScheme,
                                                                 typeof (CefSchemeCapiDelegates.AddCustomSchemeCallback));

            var name = new StringUtf16(descriptor.Scheme);

            var isStandard = descriptor.SchemeProperties.HasFlag(SchemeProperties.Standard) ? 1 : 0;
            var isLocal = descriptor.SchemeProperties.HasFlag(SchemeProperties.Local) ? 1 : 0;
            var isDisplayIsolated = descriptor.SchemeProperties.HasFlag(SchemeProperties.DisplayIsolated) ? 1 : 0;

            var result = function(NativeHandle, name.NativeHandle, isStandard, isLocal, isDisplayIsolated);
            var success = Convert.ToBoolean(result);
            if (!success) {
                Debug.WriteLine("Error registering custom scheme '{0}'. See debug.log for details.", descriptor.Scheme);
            }
        }
    }
}
