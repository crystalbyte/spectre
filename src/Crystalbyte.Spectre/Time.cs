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
    public sealed class Time : NativeTypeAdapter {
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
