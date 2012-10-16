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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public static class JavaScriptEnumerationExtensions {
        private static readonly int PointerSize = Marshal.SizeOf(typeof (IntPtr));

        public static IntPtr ToUnmanagedArray(this IEnumerable<JavaScriptObject> items) {
            var enumerable = items as JavaScriptObject[] ?? items.ToArray();
            var count = enumerable.Count();
            var handle = Marshal.AllocHGlobal(count*PointerSize);
            var i = 0;
            enumerable.ForEach(x => Marshal.WriteIntPtr(handle, i++*PointerSize, x.NativeHandle));
            return handle;
        }
    }
}
