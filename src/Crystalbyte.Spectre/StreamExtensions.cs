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
