#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class Browser : Adapter {
        private Browser(IntPtr handle)
            : base(typeof (CefBrowser), true) {
            Frames = new FrameCollection(this);
            NativeHandle = handle;
        }

        internal BrowserHost Host {
            get {
                var reflection = MarshalFromNative<CefBrowser>();
                var function = (GetHostCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetHost, typeof (GetHostCallback));
                var handle = function(NativeHandle);
                return BrowserHost.FromHandle(handle);
            }
        }

        public FrameCollection Frames { get; private set; }

        public IEnumerable<string> FrameNames {
            get {
                var reflection = MarshalFromNative<CefBrowser>();
                var action = (GetFrameNamesCallback)
                             Marshal.GetDelegateForFunctionPointer(reflection.GetFrameNames,
                                                                   typeof (GetFrameNamesCallback));
                var target = new Utf16StringCollection();
                action(NativeHandle, target.NativeHandle);
                return target;
            }
        }

        public IEnumerable<string> FrameIds {
            get {
                var reflection = MarshalFromNative<CefBrowser>();
                var action = (GetFrameIdentifiersCallback)
                             Marshal.GetDelegateForFunctionPointer(reflection.GetFrameIdentifiers,
                                                                   typeof (GetFrameIdentifiersCallback));
                var target = new Utf16StringCollection();
                long count;
                action(NativeHandle, out count, target.NativeHandle);
                return target;
            }
        }

        public Frame FocusedFrame {
            get {
                var reflection = MarshalFromNative<CefBrowser>();
                var function = (GetFocusedFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetFocusedFrame,
                                                                     typeof (GetFocusedFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public Frame MainFrame {
            get {
                var reflection = MarshalFromNative<CefBrowser>();
                var function = (GetMainFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetMainFrame,
                                                                     typeof (GetMainFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public static Browser FromHandle(IntPtr handle) {
            return new Browser(handle);
        }
    }
}