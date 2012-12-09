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
using System.Security;

#endregion

namespace Crystalbyte.Spectre.Projections {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefResourceHandler {
        public CefBase Base;
        public IntPtr ProcessRequest;
        public IntPtr GetResponseHeaders;
        public IntPtr ReadResponse;
        public IntPtr CanGetCookie;
        public IntPtr CanSetCookie;
        public IntPtr Cancel;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefResourceHandlerCapiDelegates {
        public delegate int ProcessRequestCallback(IntPtr self, IntPtr request, IntPtr callback);

        public delegate void GetResponseHeadersCallback(
            IntPtr self, IntPtr response, ref long responseLength, IntPtr redirecturl);

        public delegate int ReadResponseCallback(
            IntPtr self, IntPtr dataOut, int bytesToRead, ref int bytesRead, IntPtr callback);

        public delegate int CanGetCookieCallback(IntPtr self, IntPtr cookie);

        public delegate int CanSetCookieCallback(IntPtr self, IntPtr cookie);

        public delegate void CancelCallback6(IntPtr self);
    }
}
