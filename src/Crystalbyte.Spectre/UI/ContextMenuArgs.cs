using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenuArgs : NativeObject {
        private ContextMenuArgs(IntPtr handle) 
            : base(typeof(CefContextMenuParams), true) {
            NativeHandle = handle;
        }

        public static ContextMenuArgs FromHandle(IntPtr handle) {
            return new ContextMenuArgs(handle);
        }

        public int X {
            get { 
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetXcoordCallback) 
                    Marshal.GetDelegateForFunctionPointer(r.GetXcoord, typeof(GetXcoordCallback));
                return function(NativeHandle);
            }
        }

        public int Y {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetYcoordCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetXcoord, typeof(GetYcoordCallback));
                return function(NativeHandle);
            }
        }

        public NodeTypes SenderType {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetTypeFlagsCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetTypeFlags, typeof(GetTypeFlagsCallback));
                return (NodeTypes) function(NativeHandle);
            }
        }

        public string LinkUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetLinkUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetLinkUrl, typeof(GetLinkUrlCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string UnfilteredLinkUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetUnfilteredLinkUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetUnfilteredLinkUrl, typeof(GetUnfilteredLinkUrlCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string SourceUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetSourceUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetSourceUrl, typeof(GetSourceUrlCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public bool IsBlockedImage {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (IsImageBlockedCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsImageBlocked, typeof(IsImageBlockedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public string PageUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetPageUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetPageUrl, typeof(GetPageUrlCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string FrameUrl {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetFrameUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetFrameUrl, typeof(GetFrameUrlCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public string FrameCharset {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetFrameCharsetCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetFrameCharset, typeof(GetFrameCharsetCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public MediaType MediaType {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetMediaTypeCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetMediaType, typeof(GetMediaTypeCallback));
                return (MediaType) function(NativeHandle);
            }
        }

        public MediaStates SupportedMediaStates {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetMediaStateFlagsCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetMediaType, typeof(GetMediaStateFlagsCallback));
                return (MediaStates) function(NativeHandle);
            }
        }

        public string SelectedText {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetSelectionTextCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetSelectionText, typeof(GetSelectionTextCallback));
                var s = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(s);
            }
        }

        public bool IsEditable {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (IsEditableCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsEditable, typeof (IsEditableCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsSpeechInputEnabled {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (IsSpeechInputEnabledCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsSpeechInputEnabled,
                                                                     typeof (IsSpeechInputEnabledCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public EditStates SupportedEditStates {
            get {
                var r = MarshalFromNative<CefContextMenuParams>();
                var function = (GetEditStateFlagsCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetEditStateFlags,
                                                                     typeof(GetEditStateFlagsCallback));
                return (EditStates) function(NativeHandle);
            }
        }
    }
}
