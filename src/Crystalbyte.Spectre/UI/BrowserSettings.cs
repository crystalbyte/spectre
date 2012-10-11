#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class BrowserSettings : NativeObject{
        private readonly bool _isOwned;

        internal BrowserSettings()
            : base(typeof (CefBrowserSettings)){
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefBrowserSettings{
                                                      Size = NativeSize
                                                  });
            _isOwned = true;
        }

        private BrowserSettings(IntPtr handle)
            : base(typeof (CefBrowserSettings)){
            NativeHandle = handle;
        }

        public bool IsFileAccessfromUrlsAllowed{
            get{
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.FileAccessFromFileUrlsAllowed;
            }
            set{
                var r = MarshalFromNative<CefBrowserSettings>();
                r.FileAccessFromFileUrlsAllowed = value;
                MarshalToNative(r);
            }
        }

        public bool IsUniversalAccessFromFileUrlsAllowed{
            get{
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.UniversalAccessFromFileUrlsAllowed;
            }
            set{
                var r = MarshalFromNative<CefBrowserSettings>();
                r.UniversalAccessFromFileUrlsAllowed = value;
                MarshalToNative(r);
            }
        }

        public bool IsWebSecurityDisabled{
            get{
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.WebSecurityDisabled;
            }
            set{
                var r = MarshalFromNative<CefBrowserSettings>();
                r.WebSecurityDisabled = value;
                MarshalToNative(r);
            }
        }

        public bool IsUserStyleSheetEnabled{
            get{
                var r = MarshalFromNative<CefBrowserSettings>();
                return r.UserStyleSheetEnabled;
            }
            set{
                var r = MarshalFromNative<CefBrowserSettings>();
                r.UserStyleSheetEnabled = value;
                MarshalToNative(r);
            }
        }

        protected override void DisposeNative(){
            base.DisposeNative();
            if (NativeHandle != IntPtr.Zero && _isOwned){
                Marshal.FreeHGlobal(NativeHandle);
            }
        }

        public static BrowserSettings FromHandle(IntPtr handle){
            return new BrowserSettings(handle);
        }
    }
}