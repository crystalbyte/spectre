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
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Web {
    public abstract class SchemeHandlerFactory : OwnedRefCountedCefTypeAdapter {
        private readonly CefSchemeCapiDelegates.CreateCallback _createCallback;

        protected SchemeHandlerFactory()
            : base(typeof (CefSchemeHandlerFactory)) {
            _createCallback = Create;
            MarshalToNative(new CefSchemeHandlerFactory {
                Base = DedicatedBase,
                Create =
                    Marshal.GetFunctionPointerForDelegate(_createCallback)
            });
        }

        private IntPtr Create(IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemename, IntPtr request) {
            var e = new CreateHandlerEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Scheme = StringUtf16.ReadString(schemename)
            };
            return OnCreateHandler(this, e).Handle;
        }

        protected abstract ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e);
    }
}
