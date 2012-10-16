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
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Spectre.Projections {
    [SuppressUnmanagedCodeSecurity]
    public static class CefStreamCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_stream_reader_create_for_file",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStreamReaderCreateForFile(IntPtr filename);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_stream_reader_create_for_data",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStreamReaderCreateForData(IntPtr data, int size);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_stream_reader_create_for_handler",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStreamReaderCreateForHandler(IntPtr handler);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_stream_writer_create_for_file",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStreamWriterCreateForFile(IntPtr filename);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_stream_writer_create_for_handler",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStreamWriterCreateForHandler(IntPtr handler);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefReadHandler {
        public CefBase Base;
        public IntPtr Read;
        public IntPtr Seek;
        public IntPtr Tell;
        public IntPtr Eof;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefStreamReader {
        public CefBase Base;
        public IntPtr Read;
        public IntPtr Seek;
        public IntPtr Tell;
        public IntPtr Eof;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefWriteHandler {
        public CefBase Base;
        public IntPtr Write;
        public IntPtr Seek;
        public IntPtr Tell;
        public IntPtr Flush;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefStreamWriter {
        public CefBase Base;
        public IntPtr Write;
        public IntPtr Seek;
        public IntPtr Tell;
        public IntPtr Flush;
    }

    public delegate int ReadCallback(IntPtr self, IntPtr ptr, int size, int n);

    public delegate int SeekCallback(IntPtr self, long offset, int whence);

    public delegate long TellCallback(IntPtr self);

    public delegate int EofCallback(IntPtr self);

    public delegate int WriteCallback(IntPtr self, IntPtr ptr, int size, int n);

    public delegate int FlushCallback(IntPtr self);
}
