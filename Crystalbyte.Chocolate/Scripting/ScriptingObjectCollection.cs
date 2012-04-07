#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    internal sealed class ScriptingObjectCollection : List<ScriptingObject> {
        private static readonly int _pointerSize = Marshal.SizeOf(typeof (IntPtr));

        public ScriptingObjectCollection(IntPtr listHandle, int argumentCount) {
            if (argumentCount < 1) {
                return;
            }
            var current = listHandle;
            for (var i = 0; i < argumentCount; i++) {
                var handle = Marshal.ReadIntPtr(current);
                Add(ScriptingObject.FromHandle(handle));
                current = new IntPtr(current.ToInt64() + _pointerSize);
            }
        }
    }
}