using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Projections;

namespace Crystalbyte.Chocolate
{
    public sealed class SchemeRegistrar : RefCountedNativeObject {
        private SchemeRegistrar(IntPtr handle) 
            : base(typeof(CefSchemeRegistrar)) {
            NativeHandle = handle;
        }

        public static SchemeRegistrar FromHandle(IntPtr handle) {
            return new SchemeRegistrar(handle);
        }

        public void Register(SchemeDescriptor descriptor) {
            var r = MarshalFromNative<CefSchemeRegistrar>();
            var function = (AddCustomSchemeCallback)
                Marshal.GetDelegateForFunctionPointer(r.AddCustomScheme, typeof (AddCustomSchemeCallback));

            var name = new StringUtf16(descriptor.Scheme);

            var isStandard = descriptor.SchemeProperties.HasFlag(SchemeProperties.Standard) ? 1 : 0;
            var isLocal = descriptor.SchemeProperties.HasFlag(SchemeProperties.Local) ? 1 : 0;
            var isDisplayIsolated = descriptor.SchemeProperties.HasFlag(SchemeProperties.DisplayIsolated) ? 1 : 0;

            var result = function(NativeHandle, name.NativeHandle, isStandard, isLocal, isDisplayIsolated);
            var success = Convert.ToBoolean(result);
            if (!success) {
                Debug.WriteLine("Error registering custom scheme '{0}'",descriptor.Scheme);
            } else {
                descriptor.OnRegistered(EventArgs.Empty);
            }
        }
    }
}
