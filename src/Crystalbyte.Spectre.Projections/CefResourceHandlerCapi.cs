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

    public delegate int ProcessRequestCallback(IntPtr self, IntPtr request, IntPtr callback);

    public delegate void GetResponseHeadersCallback(
        IntPtr self, IntPtr response, out int responseLength, IntPtr redirecturl);

    public delegate int ReadResponseCallback(
        IntPtr self, IntPtr dataOut, int bytesToRead, out int bytesRead, IntPtr callback);

    public delegate int CanGetCookieCallback(IntPtr self, IntPtr cookie);

    public delegate int CanSetCookieCallback(IntPtr self, IntPtr cookie);
}
