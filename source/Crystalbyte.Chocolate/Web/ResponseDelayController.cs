using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Projections;

namespace Crystalbyte.Chocolate.Web
{

    public sealed class ResponseDelayController : NativeObject
    {
        private ResponseDelayController(IntPtr handle)
            : base(typeof(CefCallback), true)
        {
            NativeHandle = handle;
        }

        public static ResponseDelayController FromHandle(IntPtr handle)
        {
            return new ResponseDelayController(handle);
        }

        public bool IsPaused { get; private set; }

        public void Resume()
        {
            var r = MarshalFromNative<CefCallback>();
            var action = (ContCallback)Marshal.GetDelegateForFunctionPointer(r.Cont, typeof(ContCallback));
            action(NativeHandle);
        }

        public void Pause()
        {
            IsPaused = true;
        }
    }
}
