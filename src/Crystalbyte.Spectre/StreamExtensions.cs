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
using System.IO;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre {
    public static class StreamExtensions {
        /// <summary>
        ///   This method will write the contents of the stream into unmanaged memory and return its handle.
        ///   The maximum size is currently limited to 2GB.
        /// </summary>
        /// <param name="stream"> The managed input stream. </param>
        /// <returns> Handle to the unmanaged copy. </returns>
        public static IntPtr ToUnmanagedMemory(this Stream stream) {
            var size = Marshal.SizeOf(typeof (byte));
            var length = (int) stream.Length;
            var handle = Marshal.AllocHGlobal(length*size);
            using (var br = new BinaryReader(stream)) {
                var bytes = br.ReadBytes(length);
                Marshal.Copy(bytes, 0, handle, length);
            }
            return handle;
        }

        public static string ToUtf8String(this Stream stream) {
            using (var reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }
}
