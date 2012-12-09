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
    public struct CefJsdialogCallback {
        public CefBase Base;
        public IntPtr Cont;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefJsdialogHandler {
        public CefBase Base;
        public IntPtr OnJsdialog;
        public IntPtr OnBeforeUnloadDialog;
        public IntPtr OnResetDialogState;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefJsdialogHandlerCapiDelegates {
        public delegate void ContCallback6(IntPtr self, int success, IntPtr userInput);

        public delegate int OnJsdialogCallback(
            IntPtr self, IntPtr browser, IntPtr originUrl, IntPtr acceptLang, CefJsdialogType dialogType,
            IntPtr messageText, IntPtr defaultPromptText, IntPtr callback, ref int suppressMessage);

        public delegate int OnBeforeUnloadDialogCallback(
            IntPtr self, IntPtr browser, IntPtr messageText, int isReload, IntPtr callback);

        public delegate void OnResetDialogStateCallback(IntPtr self, IntPtr browser);
    }
}
