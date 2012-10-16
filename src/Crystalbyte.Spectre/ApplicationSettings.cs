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
    public sealed class ApplicationSettings : NativeObject {
        public ApplicationSettings()
            : base(typeof (CefSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefSettings {
                Size = NativeSize,
                LogSeverity = CefLogSeverity.LogseverityInfo
            });
        }

        public string BrowserSubprocessPath {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(reflection.BrowserSubprocessPath.Str);
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.BrowserSubprocessPath = new CefStringUtf16 {
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(reflection);
            }
        }

        public string LocalesDirPath {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(reflection.LocalesDirPath.Str);
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.LocalesDirPath = new CefStringUtf16 {
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(reflection);
            }
        }

        public string Locale {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return Marshal.PtrToStringUni(reflection.Locale.Str);
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.Locale = new CefStringUtf16 {
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                };
                MarshalToNative(reflection);
            }
        }

        public LogSeverity LogSeverity {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return (LogSeverity) reflection.LogSeverity;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.LogSeverity = (CefLogSeverity) value;
                MarshalToNative(reflection);
            }
        }

        public bool IsMessageLoopMultiThreaded {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return reflection.MultiThreadedMessageLoop;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.MultiThreadedMessageLoop = value;
                MarshalToNative(reflection);
            }
        }

        public bool IsSingleProcess {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return reflection.SingleProcess;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.SingleProcess = value;
                MarshalToNative(reflection);
            }
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}
