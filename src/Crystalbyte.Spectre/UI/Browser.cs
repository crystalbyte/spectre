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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class Browser : RefCountedNativeTypeAdapter {
        private Browser(IntPtr handle)
            : base(typeof (CefBrowser)) {
            Frames = new FrameCollection(this);
            Handle = handle;
        }

        internal BrowserHost Host {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.GetHostCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetHost,
                                                                     typeof (CefBrowserCapiDelegates.GetHostCallback));
                var handle = function(Handle);
                return BrowserHost.FromHandle(handle);
            }
        }

        public FrameCollection Frames { get; private set; }

        public IEnumerable<string> FrameNames {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var action = (CefBrowserCapiDelegates.GetFrameNamesCallback)
                             Marshal.GetDelegateForFunctionPointer(r.GetFrameNames,
                                                                   typeof (CefBrowserCapiDelegates.GetFrameNamesCallback
                                                                       ));
                var target = new StringUtf16List();
                action(Handle, target.Handle);
                return target;
            }
        }

        public IEnumerable<string> FrameIds {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var action = (CefBrowserCapiDelegates.GetFrameIdentifiersCallback)
                             Marshal.GetDelegateForFunctionPointer(r.GetFrameIdentifiers,
                                                                   typeof (
                                                                       CefBrowserCapiDelegates.
                                                                       GetFrameIdentifiersCallback));
                var target = new StringUtf16List();
                var count = 0;
                var identifiers = target.Handle.ToInt64();
                action(Handle, ref count, ref identifiers);
                return target;
            }
        }

        public Frame FocusedFrame {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.GetFocusedFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFocusedFrame,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetFocusedFrameCallback
                                                                         ));
                var handle = function(Handle);
                return Frame.FromHandle(handle);
            }
        }

        public Frame MainFrame {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.GetMainFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMainFrame,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetMainFrameCallback));
                var handle = function(Handle);
                return Frame.FromHandle(handle);
            }
        }

        public bool CanGoBack {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.CanGoBackCallback)
                               Marshal.GetDelegateForFunctionPointer(r.CanGoBack,
                                                                     typeof (CefBrowserCapiDelegates.CanGoBackCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool CanGoForward {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.CanGoForwardCallback)
                               Marshal.GetDelegateForFunctionPointer(r.CanGoForward,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.CanGoForwardCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool HasDocument {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.HasDocumentCallback)
                               Marshal.GetDelegateForFunctionPointer(r.HasDocument,
                                                                     typeof (CefBrowserCapiDelegates.HasDocumentCallback
                                                                         ));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public long Id {
            get {
                var r = MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.GetIdentifierCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetIdentifier,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetIdentifierCallback));
                return function(Handle);
            }
        }

        public void GoBack() {
            var r = MarshalFromNative<CefBrowser>();
            var action = (CefBrowserCapiDelegates.GoBackCallback)
                         Marshal.GetDelegateForFunctionPointer(r.GoBack, typeof (CefBrowserCapiDelegates.GoBackCallback));
            action(Handle);
        }

        public void GoForward() {
            var r = MarshalFromNative<CefBrowser>();
            var action = (CefBrowserCapiDelegates.GoForwardCallback)
                         Marshal.GetDelegateForFunctionPointer(r.GoForward,
                                                               typeof (CefBrowserCapiDelegates.GoForwardCallback));
            action(Handle);
        }

        public void CancelNavigation() {
            var r = MarshalFromNative<CefBrowser>();
            var action = (CefBrowserCapiDelegates.StopLoadCallback)
                         Marshal.GetDelegateForFunctionPointer(r.StopLoad,
                                                               typeof (CefBrowserCapiDelegates.StopLoadCallback));
            action(Handle);
        }

        public void Refresh(bool ignoreCache = false) {
            var r = MarshalFromNative<CefBrowser>();
            CefBrowserCapiDelegates.ReloadCallback action;
            if (ignoreCache) {
                action = (CefBrowserCapiDelegates.ReloadCallback)
                         Marshal.GetDelegateForFunctionPointer(r.ReloadIgnoreCache,
                                                               typeof (CefBrowserCapiDelegates.ReloadCallback));
            }
            else {
                action = (CefBrowserCapiDelegates.ReloadCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Reload, typeof (CefBrowserCapiDelegates.ReloadCallback));
            }
            action(Handle);
        }

        public static Browser Current {
            get { return ScriptingContext.Current.Browser; }
        }

        public void SendIpcMessage(ProcessType target, IpcMessage message) {
            var r = MarshalFromNative<CefBrowser>();
            var action = (CefBrowserCapiDelegates.SendProcessMessageCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SendProcessMessage,
                                                               typeof (
                                                                   CefBrowserCapiDelegates.SendProcessMessageCallback));
            action(Handle, (CefProcessId) target, message.Handle);
        }

        public static Browser FromHandle(IntPtr handle) {
            return new Browser(handle);
        }
    }
}
