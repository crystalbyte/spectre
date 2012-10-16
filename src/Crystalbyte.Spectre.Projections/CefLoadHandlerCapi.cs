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
    public struct CefLoadHandler {
        public CefBase Base;
        public IntPtr OnLoadStart;
        public IntPtr OnLoadEnd;
        public IntPtr OnLoadError;
        public IntPtr OnRenderProcessTerminated;
        public IntPtr OnPluginCrashed;
    }

    public delegate void OnLoadStartCallback(IntPtr self, IntPtr browser, IntPtr frame);

    public delegate void OnLoadEndCallback(IntPtr self, IntPtr browser, IntPtr frame, int httpstatuscode);

    public delegate void OnLoadErrorCallback(
        IntPtr self, IntPtr browser, IntPtr frame, CefErrorcode errorcode, IntPtr errortext, IntPtr failedurl);

    public delegate void OnRenderProcessTerminatedCallback(IntPtr self, IntPtr browser, CefTerminationStatus status);

    public delegate void OnPluginCrashedCallback(IntPtr self, IntPtr browser, IntPtr pluginPath);
}
