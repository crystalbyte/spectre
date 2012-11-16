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
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenuArgs : RefCountedNativeTypeAdapter {
        private ContextMenuArgs(IntPtr handle)
            : base(typeof (CefContextMenuParams)) {
            Handle = handle;
        }

        public int X {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetXcoordCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetXcoord,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetXcoordCallback));
                return function(Handle);
            }
        }

        public int Y {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetYcoordCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetXcoord,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetYcoordCallback));
                return function(Handle);
            }
        }

        public NodeTypes SenderType {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetTypeFlagsCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetTypeFlags,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetTypeFlagsCallback));
                return (NodeTypes) function(Handle);
            }
        }

        public string LinkUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetLinkUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetLinkUrl,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetLinkUrlCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string UnfilteredLinkUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetUnfilteredLinkUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetUnfilteredLinkUrl,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetUnfilteredLinkUrlCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string SourceUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetSourceUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetSourceUrl,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetSourceUrlCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public bool IsBlockedImage {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.IsImageBlockedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsImageBlocked,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         IsImageBlockedCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public string PageUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetPageUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetPageUrl,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetPageUrlCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string FrameUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetFrameUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrameUrl,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetFrameUrlCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string FrameCharset {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetFrameCharsetCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrameCharset,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetFrameCharsetCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public MediaType MediaType {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetMediaTypeCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMediaType,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetMediaTypeCallback));
                return (MediaType) function(Handle);
            }
        }

        public MediaStates SupportedMediaStates {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetMediaStateFlagsCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMediaType,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetMediaStateFlagsCallback));
                return (MediaStates) function(Handle);
            }
        }

        public string SelectedText {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetSelectionTextCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetSelectionText,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetSelectionTextCallback));
                var s = function(Handle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public bool IsEditable {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.IsEditableCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsEditable,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         IsEditableCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsSpeechInputEnabled {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.IsSpeechInputEnabledCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsSpeechInputEnabled,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         IsSpeechInputEnabledCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public EditStates SupportedEditStates {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (CefContextMenuHandlerCapiDelegates.GetEditStateFlagsCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetEditStateFlags,
                                                                     typeof (
                                                                         CefContextMenuHandlerCapiDelegates.
                                                                         GetEditStateFlagsCallback));
                return (EditStates) function(Handle);
            }
        }

        public static ContextMenuArgs FromHandle(IntPtr handle) {
            return new ContextMenuArgs(handle);
        }
    }
}
