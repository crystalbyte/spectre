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

namespace Crystalbyte.Spectre {
    public sealed class Time : CefTypeAdapter {
        private readonly bool _isOwned;

        public Time(DateTime time)
            : base(typeof (CefTime)) {
            Handle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefTime {
                Millisecond = time.Millisecond,
                Second = time.Second,
                Minute = time.Minute,
                Hour = time.Hour,
                Month = time.Month,
                Year = time.Year,
                DayOfMonth = time.Day,
                DayOfWeek = (int) time.DayOfWeek
            });
            _isOwned = true;
        }

        private Time(IntPtr handle)
            : base(typeof (CefTime)) {
            Handle = handle;
        }

        public static Time FromHandle(IntPtr handle) {
            return new Time(handle);
        }

        public DateTime ToDateTime() {
            var reflection = MarshalFromNative<CefTime>();
            return new DateTime(reflection.Year,
                                reflection.Month,
                                reflection.DayOfMonth,
                                reflection.Hour,
                                reflection.Minute,
                                reflection.Second,
                                reflection.Millisecond);
        }

        protected override void DisposeNative() {
            if (Handle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(Handle);
            }
            base.DisposeNative();
        }
    }
}
