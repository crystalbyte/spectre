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
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Projections {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefClient {
        public CefBase Base;
        public IntPtr GetContextMenuHandler;
        public IntPtr GetDisplayHandler;
        public IntPtr GetDownloadHandler;
        public IntPtr GetFocusHandler;
        public IntPtr GetGeolocationHandler;
        public IntPtr GetJsdialogHandler;
        public IntPtr GetKeyboardHandler;
        public IntPtr GetLifeSpanHandler;
        public IntPtr GetLoadHandler;
        public IntPtr GetRequestHandler;
        public IntPtr OnProcessMessageReceived;
    }

    public delegate IntPtr GetContextMenuHandlerCallback(IntPtr self);

    public delegate IntPtr GetDisplayHandlerCallback(IntPtr self);

    public delegate IntPtr GetDownloadHandlerCallback(IntPtr self);

    public delegate IntPtr GetFocusHandlerCallback(IntPtr self);

    public delegate IntPtr GetGeolocationHandlerCallback(IntPtr self);

    public delegate IntPtr GetJsdialogHandlerCallback(IntPtr self);

    public delegate IntPtr GetKeyboardHandlerCallback(IntPtr self);

    public delegate IntPtr GetLifeSpanHandlerCallback(IntPtr self);

    public delegate IntPtr GetLoadHandlerCallback(IntPtr self);

    public delegate IntPtr GetRequestHandlerCallback(IntPtr self);

    public delegate int OnProcessMessageReceivedCallback(
        IntPtr self, IntPtr browser, CefProcessId sourceProcess, IntPtr message);
}
