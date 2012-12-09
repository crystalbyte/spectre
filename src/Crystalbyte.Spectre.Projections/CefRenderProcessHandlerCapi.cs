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
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Projections {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefRenderProcessHandler {
        public CefBase Base;
        public IntPtr OnRenderThreadCreated;
        public IntPtr OnWebKitInitialized;
        public IntPtr OnBrowserCreated;
        public IntPtr OnBrowserDestroyed;
        public IntPtr OnBeforeNavigation;
        public IntPtr OnContextCreated;
        public IntPtr OnContextReleased;
        public IntPtr OnUncaughtException;
        public IntPtr OnFocusedNodeChanged;
        public IntPtr OnProcessMessageReceived;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefRenderProcessHandlerCapiDelegates {
        public delegate void OnRenderThreadCreatedCallback(IntPtr self);

        public delegate void OnWebKitInitializedCallback(IntPtr self);

        public delegate void OnBrowserCreatedCallback(IntPtr self, IntPtr browser);

        public delegate void OnBrowserDestroyedCallback(IntPtr self, IntPtr browser);

        public delegate int OnBeforeNavigationCallback(
            IntPtr self, IntPtr browser, IntPtr frame, IntPtr request, CefNavigationType navigationType, int isRedirect);

        public delegate void OnContextCreatedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context);

        public delegate void OnContextReleasedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context);

        public delegate void OnUncaughtExceptionCallback(
            IntPtr self, IntPtr browser, IntPtr frame, IntPtr context, IntPtr exception, IntPtr stacktrace);

        public delegate void OnFocusedNodeChangedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr node);

        public delegate int OnProcessMessageReceivedCallback2(
            IntPtr self, IntPtr browser, CefProcessId sourceProcess, IntPtr message);
    }
}
