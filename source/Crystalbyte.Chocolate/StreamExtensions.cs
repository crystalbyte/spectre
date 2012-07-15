#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.IO;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate {
    public static class StreamExtensions {
        /// <summary>
        ///   This method will write the contents of the stream into unmanaged memory and return its handle.
        ///   The maximum size is currently limited to 2GB.
        /// </summary>
        /// <param name="stream"> The stream to be marshalled. </param>
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
    }
}