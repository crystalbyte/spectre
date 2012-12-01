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
    public sealed class ResourceBundleHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefResourceBundleHandlerCapiDelegates.GetDataResourceCallback _getDataResourceCallback;
        private readonly CefResourceBundleHandlerCapiDelegates.GetLocalizedStringCallback _getlocalizedStringCallback;
        //private readonly AppDelegate _appDelegate;

        public ResourceBundleHandler(RendererDelegate appDelegate)
            : base(typeof (CefResourceBundleHandler)) {
            //_appDelegate = appDelegate;
            _getDataResourceCallback = OnGetDataResource;
            _getlocalizedStringCallback = OnGetLocalizedString;

            MarshalToNative(new CefResourceBundleHandler {
                Base = DedicatedBase,
                GetDataResource = Marshal.GetFunctionPointerForDelegate(_getDataResourceCallback),
                GetLocalizedString = Marshal.GetFunctionPointerForDelegate(_getlocalizedStringCallback)
            });
        }

        private int OnGetLocalizedString(IntPtr self, int messageId, IntPtr @string) {
            return 0;
        }

        private int OnGetDataResource(IntPtr self, int resourceId, IntPtr data, ref int dataSize) {
            return 0;
        }
    }
}
