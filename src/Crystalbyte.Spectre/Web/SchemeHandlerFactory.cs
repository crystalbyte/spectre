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
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Web {
    public abstract class SchemeHandlerFactory : OwnedRefCountedNativeObject {
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
            return OnCreateHandler(this, e).NativeHandle;
        }

        protected abstract ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e);
    }
}
