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
    public struct CefRenderProcessHandler {
        public CefBase Base;
        public IntPtr OnRenderThreadCreated;
        public IntPtr OnWebKitInitialized;
        public IntPtr OnBrowserCreated;
        public IntPtr OnBrowserDestroyed;
        public IntPtr OnContextCreated;
        public IntPtr OnContextReleased;
        public IntPtr OnFocusedNodeChanged;
        public IntPtr OnProcessMessageReceived;
    }

    public delegate void OnRenderThreadCreatedCallback(IntPtr self);

    public delegate void OnWebKitInitializedCallback(IntPtr self);

    public delegate void OnBrowserCreatedCallback(IntPtr self, IntPtr browser);

    public delegate void OnBrowserDestroyedCallback(IntPtr self, IntPtr browser);

    public delegate void OnContextCreatedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context);

    public delegate void OnContextReleasedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context);

    public delegate void OnFocusedNodeChangedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr node);
}
