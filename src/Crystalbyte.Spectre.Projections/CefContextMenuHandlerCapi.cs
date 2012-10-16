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
    public struct CefContextMenuHandler {
        public CefBase Base;
        public IntPtr OnBeforeContextMenu;
        public IntPtr OnContextMenuCommand;
        public IntPtr OnContextMenuDismissed;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefContextMenuParams {
        public CefBase Base;
        public IntPtr GetXcoord;
        public IntPtr GetYcoord;
        public IntPtr GetTypeFlags;
        public IntPtr GetLinkUrl;
        public IntPtr GetUnfilteredLinkUrl;
        public IntPtr GetSourceUrl;
        public IntPtr IsImageBlocked;
        public IntPtr GetPageUrl;
        public IntPtr GetFrameUrl;
        public IntPtr GetFrameCharset;
        public IntPtr GetMediaType;
        public IntPtr CefCallbackGetMediaStateFlags;
        public IntPtr GetSelectionText;
        public IntPtr IsEditable;
        public IntPtr IsSpeechInputEnabled;
        public IntPtr GetEditStateFlags;
    }

    public delegate void OnBeforeContextMenuCallback(
        IntPtr self, IntPtr browser, IntPtr frame, IntPtr @params, IntPtr model);

    public delegate int OnContextMenuCommandCallback(
        IntPtr self, IntPtr browser, IntPtr frame, IntPtr @params, int commandId, CefEventFlags eventFlags);

    public delegate void OnContextMenuDismissedCallback(IntPtr self, IntPtr browser, IntPtr frame);

    public delegate int GetXcoordCallback(IntPtr self);

    public delegate int GetYcoordCallback(IntPtr self);

    public delegate CefContextMenuTypeFlags GetTypeFlagsCallback(IntPtr self);

    public delegate IntPtr GetLinkUrlCallback(IntPtr self);

    public delegate IntPtr GetUnfilteredLinkUrlCallback(IntPtr self);

    public delegate IntPtr GetSourceUrlCallback(IntPtr self);

    public delegate int IsImageBlockedCallback(IntPtr self);

    public delegate IntPtr GetPageUrlCallback(IntPtr self);

    public delegate IntPtr GetFrameUrlCallback(IntPtr self);

    public delegate IntPtr GetFrameCharsetCallback(IntPtr self);

    public delegate CefContextMenuMediaType GetMediaTypeCallback(IntPtr self);

    public delegate CefContextMenuMediaStateFlags GetMediaStateFlagsCallback(IntPtr self);

    public delegate IntPtr GetSelectionTextCallback(IntPtr self);

    public delegate int IsEditableCallback(IntPtr self);

    public delegate int IsSpeechInputEnabledCallback(IntPtr self);

    public delegate CefContextMenuEditStateFlags GetEditStateFlagsCallback(IntPtr self);
}
