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

namespace Crystalbyte.Spectre.Projections.Internal {
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowsCefMainArgs {
        public IntPtr Instance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowsCefWindowInfo {
        public uint ExStyle;
        public CefStringUtf16 WindowName;
        public uint Style;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public IntPtr ParentWindow;
        public IntPtr Menu;
        public bool TransparentPainting;
        public IntPtr Window;
    }
}
