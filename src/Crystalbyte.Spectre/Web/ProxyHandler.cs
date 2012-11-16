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
    public sealed class ProxyHandler : OwnedRefCountedNativeTypeAdapter {
        private readonly AppDelegate _delegate;
        private readonly CefProxyHandlerCapiDelegates.GetProxyForUrlCallback _getProxyForUrlCallback;

        public ProxyHandler(AppDelegate @delegate)
            : base(typeof (CefProxyHandler)) {
            _delegate = @delegate;
            _getProxyForUrlCallback = GetProxyForUrl;
            MarshalToNative(new CefProxyHandler {
                Base = DedicatedBase,
                GetProxyForUrl =
                    Marshal.GetFunctionPointerForDelegate(_getProxyForUrlCallback)
            });
        }

        private void GetProxyForUrl(IntPtr self, IntPtr url, IntPtr proxyinfo) {
            var e = new ProxyUrlEventArgs {
                Url = StringUtf16.ReadString(url)
            };
            _delegate.OnProxyForUrlRequested(e);
        }
    }
}
