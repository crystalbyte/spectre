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
    public struct CefLifeSpanHandler {
        public CefBase Base;
        public IntPtr OnBeforePopup;
        public IntPtr OnAfterCreated;
        public IntPtr RunModal;
        public IntPtr DoClose;
        public IntPtr OnBeforeClose;
    }

    public delegate int OnBeforePopupCallback(
        IntPtr self, IntPtr parentbrowser, IntPtr popupfeatures, IntPtr windowinfo, IntPtr url, IntPtr client,
        IntPtr settings);

    public delegate void OnAfterCreatedCallback(IntPtr self, IntPtr browser);

    public delegate int RunModalCallback(IntPtr self, IntPtr browser);

    public delegate int DoCloseCallback(IntPtr self, IntPtr browser);

    public delegate void OnBeforeCloseCallback(IntPtr self, IntPtr browser);
}
