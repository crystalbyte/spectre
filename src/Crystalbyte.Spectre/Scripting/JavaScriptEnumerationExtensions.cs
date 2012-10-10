using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Crystalbyte.Spectre.Scripting {
    public static class JavaScriptEnumerationExtensions {
        private static readonly int PointerSize = Marshal.SizeOf(typeof(IntPtr));

        public static IntPtr ToUnmanagedArray(this IEnumerable<JavaScriptObject> items) {
            var enumerable = items as JavaScriptObject[] ?? items.ToArray();
            var count = enumerable.Count();
            var handle = Marshal.AllocHGlobal(count * PointerSize);
            var i = 0;
            enumerable.ForEach(x => Marshal.WriteIntPtr(handle, i++ * PointerSize, x.NativeHandle));
            return handle;
        }
    }
}
