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
