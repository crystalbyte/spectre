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
            enumerable.ForEach(x => Marshal.WriteIntPtr(handle, i++*PointerSize, x.Handle));
            return handle;
        }
    }
}
