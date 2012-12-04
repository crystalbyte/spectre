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
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class SchemeRegistrar : OwnedRefCountedCefTypeAdapter {
        private SchemeRegistrar(IntPtr handle)
            : base(typeof (CefSchemeRegistrar)) {
            Handle = handle;
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

            var result = function(Handle, name.Handle, isStandard, isLocal, isDisplayIsolated);
            var success = Convert.ToBoolean(result);
            if (!success) {
                Debug.WriteLine("Error registering custom scheme '{0}'. See debug.log for details.", descriptor.Scheme);
            }
        }
    }
}
