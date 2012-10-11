﻿#region Using directives

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class Browser : RefCountedNativeObject{
        private Browser(IntPtr handle)
            : base(typeof (CefBrowser)){
            Frames = new FrameCollection(this);
            NativeHandle = handle;
        }

        internal BrowserHost Host{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (GetHostCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetHost, typeof (GetHostCallback));
                var handle = function(NativeHandle);
                return BrowserHost.FromHandle(handle);
            }
        }

        public FrameCollection Frames { get; private set; }

        public IEnumerable<string> FrameNames{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var action = (GetFrameNamesCallback)
                             Marshal.GetDelegateForFunctionPointer(r.GetFrameNames,
                                                                   typeof (GetFrameNamesCallback));
                var target = new StringUtf16Collection();
                action(NativeHandle, target.NativeHandle);
                return target;
            }
        }

        public IEnumerable<string> FrameIds{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var action = (GetFrameIdentifiersCallback)
                             Marshal.GetDelegateForFunctionPointer(r.GetFrameIdentifiers,
                                                                   typeof (GetFrameIdentifiersCallback));
                var target = new StringUtf16Collection();
                long count;
                action(NativeHandle, out count, target.NativeHandle);
                return target;
            }
        }

        public Frame FocusedFrame{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (GetFocusedFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFocusedFrame,
                                                                     typeof (GetFocusedFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public Frame MainFrame{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (GetMainFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMainFrame,
                                                                     typeof (GetMainFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public bool CanGoBack{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (CanGoBackCallback)
                               Marshal.GetDelegateForFunctionPointer(r.CanGoBack, typeof (CanGoBackCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool CanGoForward{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (CanGoForwardCallback)
                               Marshal.GetDelegateForFunctionPointer(r.CanGoForward, typeof (CanGoForwardCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool HasDocument{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (HasDocumentCallback)
                               Marshal.GetDelegateForFunctionPointer(r.HasDocument, typeof (HasDocumentCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public long Id{
            get{
                var r = MarshalFromNative<CefBrowser>();
                var function = (GetIdentifierCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetIdentifier, typeof (GetIdentifierCallback));
                return function(NativeHandle);
            }
        }

        public void GoBack(){
            var r = MarshalFromNative<CefBrowser>();
            var action = (GoBackCallback)
                         Marshal.GetDelegateForFunctionPointer(r.GoBack, typeof (GoBackCallback));
            action(NativeHandle);
        }

        public void GoForward(){
            var r = MarshalFromNative<CefBrowser>();
            var action = (GoForwardCallback)
                         Marshal.GetDelegateForFunctionPointer(r.GoForward, typeof (GoForwardCallback));
            action(NativeHandle);
        }

        public void CancelNavigation(){
            var r = MarshalFromNative<CefBrowser>();
            var action = (StopLoadCallback)
                         Marshal.GetDelegateForFunctionPointer(r.StopLoad, typeof (StopLoadCallback));
            action(NativeHandle);
        }

        public void Refresh(bool ignoreCache = false){
            var r = MarshalFromNative<CefBrowser>();
            ReloadCallback action;
            if (ignoreCache){
                action = (ReloadCallback)
                         Marshal.GetDelegateForFunctionPointer(r.ReloadIgnoreCache, typeof (ReloadCallback));
            }
            else{
                action = (ReloadCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Reload, typeof (ReloadCallback));
            }
            action(NativeHandle);
        }

        public void SendIpcMessage(ProcessType target, IpcMessage message){
            var r = MarshalFromNative<CefBrowser>();
            var action = (SendProcessMessageCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SendProcessMessage, typeof (SendProcessMessageCallback));
            action(NativeHandle, (CefProcessId) target, message.NativeHandle);
        }

        public static Browser FromHandle(IntPtr handle){
            return new Browser(handle);
        }
    }
}