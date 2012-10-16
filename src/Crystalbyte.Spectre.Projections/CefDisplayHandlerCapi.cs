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
    public struct CefDisplayHandler {
        public CefBase Base;
        public IntPtr OnLoadingStateChange;
        public IntPtr OnAddressChange;
        public IntPtr OnTitleChange;
        public IntPtr OnTooltip;
        public IntPtr OnStatusMessage;
        public IntPtr OnConsoleMessage;
    }

    public delegate void OnLoadingStateChangeCallback(
        IntPtr self, IntPtr browser, int isloading, int cangoback, int cangoforward);

    public delegate void OnAddressChangeCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr url);

    public delegate void OnTitleChangeCallback(IntPtr self, IntPtr browser, IntPtr title);

    public delegate int OnTooltipCallback(IntPtr self, IntPtr browser, IntPtr text);

    public delegate void OnStatusMessageCallback(IntPtr self, IntPtr browser, IntPtr value);

    public delegate int OnConsoleMessageCallback(IntPtr self, IntPtr browser, IntPtr message, IntPtr source, int line);
}
