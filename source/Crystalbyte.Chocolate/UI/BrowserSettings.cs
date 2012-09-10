#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class BrowserSettings : NativeObject {
        private readonly bool _isOwned;

        internal BrowserSettings()
            : base(typeof (CefBrowserSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefBrowserSettings {
                Size = NativeSize
            });
            _isOwned = true;
        }

        private BrowserSettings(IntPtr handle)
            : base(typeof (CefBrowserSettings)) {
            NativeHandle = handle;
        }

        protected override void DisposeNative() {
            base.DisposeNative();
            if (NativeHandle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(NativeHandle);
            }
        }

        public static BrowserSettings FromHandle(IntPtr handle) {
            return new BrowserSettings(handle);
        }

        public bool IsFileAccessfromUrlsAllowed {
            get {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.FileAccessFromFileUrlsAllowed;
            }
            set {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.FileAccessFromFileUrlsAllowed = value;
                MarshalToNative(r);
            }
        }

        public bool IsUniversalAccessFromFileUrlsAllowed
        {
            get
            {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.UniversalAccessFromFileUrlsAllowed;
            }
            set
            {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.UniversalAccessFromFileUrlsAllowed = value;
                MarshalToNative(r);
            }
        }

        public bool IsWebSecurityDisabled
        {
            get
            {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.WebSecurityDisabled;
            }
            set
            {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.WebSecurityDisabled = value;
                MarshalToNative(r);
            }
        }

        public bool IsUserStyleSheetEnabled
        {
            get
            {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.UserStyleSheetEnabled;
            }
            set
            {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.UserStyleSheetEnabled = value;
                MarshalToNative(r);
            }
        }
    }
}
