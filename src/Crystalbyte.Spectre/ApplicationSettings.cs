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

namespace Crystalbyte.Spectre {
    public sealed class ApplicationSettings : CefTypeAdapter {
        public ApplicationSettings()
            : base(typeof(CefSettings)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefSettings {
                Size = NativeSize,
                LogSeverity = CefLogSeverity.LogseverityInfo
            });

            // TODO: Implementation is incomplete, must implement free calls for all containing strings on dispose
        }

        public string ProductVersion {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.ProductVersion.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.ProductVersion = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public int RemoteDebuggingPort {
            get {
                var r = MarshalFromNative<CefSettings>();
                return r.RemoteDebuggingPort;
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.RemoteDebuggingPort = value;
                MarshalToNative(r);
            }
        }

        public string CacheDirectory {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.CachePath.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.CachePath = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public string UserAgent {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.UserAgent.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.UserAgent = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public bool IsPackLoadingDisabled {
            get {
                var r = MarshalFromNative<CefSettings>();
                return r.PackLoadingDisabled;
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.PackLoadingDisabled = value;
                MarshalToNative(r);
            }
        }

        public string BrowserSubprocessPath {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.BrowserSubprocessPath.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.BrowserSubprocessPath = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public string LocalesDirectory {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.LocalesDirPath.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.LocalesDirPath = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public string Locale {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.Locale.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.Locale = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public string ResourceDirectory {
            get {
                var r = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(r.ResourcesDirPath.Str);
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.ResourcesDirPath = new CefStringUtf16 {
                    //Dtor = Marshal.GetFunctionPointerForDelegate(StringUtf16.FreeCallback),
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(r);
            }
        }

        public LogSeverity LogSeverity {
            get {
                var r = MarshalFromNative<CefSettings>();
                return (LogSeverity)r.LogSeverity;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.LogSeverity = (CefLogSeverity)value;
                MarshalToNative(reflection);
            }
        }

        public bool IsMessageLoopMultiThreaded {
            get {
                var r = MarshalFromNative<CefSettings>();
                return r.MultiThreadedMessageLoop;
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.MultiThreadedMessageLoop = value;
                MarshalToNative(r);
            }
        }

        public bool IsSingleProcess {
            get {
                var r = MarshalFromNative<CefSettings>();
                return r.SingleProcess;
            }
            set {
                var r = MarshalFromNative<CefSettings>();
                r.SingleProcess = value;
                MarshalToNative(r);
            }
        }

        protected override void DisposeNative() {
            if (Handle != IntPtr.Zero) {
                Marshal.FreeHGlobal(Handle);
            }
            base.DisposeNative();
        }
    }
}
