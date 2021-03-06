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

    [SuppressUnmanagedCodeSecurity]
    public static class CefContextMenuHandlerCapiDelegates {
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
}
