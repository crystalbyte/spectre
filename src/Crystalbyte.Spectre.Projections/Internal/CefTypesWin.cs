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

        [MarshalAs(UnmanagedType.U1)]
        public bool TransparentPainting;

        public IntPtr Window;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefTypesWinDelegates {}
}
