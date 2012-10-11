#region Using directives

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.Scripting{
    internal sealed class JavaScriptObjectCollection : List<JavaScriptObject>, IReadOnlyCollection<JavaScriptObject>{
        private static readonly int PointerSize = Marshal.SizeOf(typeof (IntPtr));

        public JavaScriptObjectCollection(IntPtr listHandle, int argumentCount){
            // TODO: List is only populated on construction
            if (argumentCount < 1){
                return;
            }
            var current = listHandle;
            for (var i = 0; i < argumentCount; i++){
                var handle = Marshal.ReadIntPtr(current);
                Add(JavaScriptObject.FromHandle(handle));
                current = new IntPtr(current.ToInt64() + PointerSize);
            }
        }
    }
}