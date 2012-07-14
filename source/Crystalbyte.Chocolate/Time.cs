#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class Time : Adapter {
        private readonly bool _isOwned;

        public Time(DateTime time)
            : base(typeof (CefTime)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
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
            NativeHandle = handle;
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
            if (NativeHandle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}