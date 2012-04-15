using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Crystalbyte.Chocolate
{
    public static class StreamExtensions
    {
        /// <summary>
        /// This method will write the contents of the stream into unmanaged memory and return its handle.
        /// The maximum size is currently limited to 2GB.
        /// </summary>
        /// <param name="stream">The stream to be marshalled.</param>
        /// <returns>Handle to the unmanaged copy.</returns>
        public static IntPtr ToUnmanagedMemory(this Stream stream) {
            var size = Marshal.SizeOf(typeof (byte));
            var length = (int) stream.Length;
            var handle = Marshal.AllocHGlobal(length * size);
            using (var br = new BinaryReader(stream)) {
                var bytes = br.ReadBytes(length);
                Marshal.Copy(bytes, 0, handle, length);
            }
            return handle;
        }
    }
}
