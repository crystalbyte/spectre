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

    public delegate int OnJsdialogCallback(
        IntPtr self, IntPtr browser, IntPtr originUrl, IntPtr acceptLang, CefJsdialogType dialogType, IntPtr messageText,
        IntPtr defaultPromptText, IntPtr callback, out int suppressMessage);

    public delegate int OnBeforeUnloadDialogCallback(
        IntPtr self, IntPtr browser, IntPtr messageText, int isReload, IntPtr callback);

    public delegate void OnResetDialogStateCallback(IntPtr self, IntPtr browser);
}
