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
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class BrowserSettings : CefTypeAdapter {
        private readonly bool _isOwned;

        internal BrowserSettings()
            : base(typeof (CefBrowserSettings)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefBrowserSettings {
                Size = NativeSize
            });
            _isOwned = true;
        }

        private BrowserSettings(IntPtr handle)
            : base(typeof (CefBrowserSettings)) {
            Handle = handle;
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

        public bool IsUniversalAccessFromFileUrlsAllowed {
            get {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.UniversalAccessFromFileUrlsAllowed;
            }
            set {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.UniversalAccessFromFileUrlsAllowed = value;
                MarshalToNative(r);
            }
        }

        public bool IsWebSecurityDisabled {
            get {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.WebSecurityDisabled;
            }
            set {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.WebSecurityDisabled = value;
                MarshalToNative(r);
            }
        }

        public bool IsUserStyleSheetEnabled {
            get {
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.UserStyleSheetEnabled;
            }
            set {
                var r = MarshalFromNative<CefBrowserSettings>();
                r.UserStyleSheetEnabled = value;
                MarshalToNative(r);
            }
        }

        protected override void DisposeNative() {
            base.DisposeNative();
            if (Handle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(Handle);
            }
        }

        public static BrowserSettings FromHandle(IntPtr handle) {
            return new BrowserSettings(handle);
        }
    }
}
