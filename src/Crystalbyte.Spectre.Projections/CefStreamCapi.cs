using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefStreamCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_stream_reader_create_for_file", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStreamReaderCreateForFile(IntPtr filename);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_stream_reader_create_for_data", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStreamReaderCreateForData(IntPtr data, int size);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_stream_reader_create_for_handler", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStreamReaderCreateForHandler(IntPtr handler);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_stream_writer_create_for_file", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStreamWriterCreateForFile(IntPtr filename);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_stream_writer_create_for_handler", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
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
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefStreamCapiDelegates {
		public delegate int ReadCallback(IntPtr self, IntPtr ptr, int size, int n);
		public delegate int SeekCallback(IntPtr self, long offset, int whence);
		public delegate long TellCallback(IntPtr self);
		public delegate int EofCallback(IntPtr self);
		public delegate int ReadCallback2(IntPtr self, IntPtr ptr, int size, int n);
		public delegate int SeekCallback2(IntPtr self, long offset, int whence);
		public delegate long TellCallback2(IntPtr self);
		public delegate int EofCallback2(IntPtr self);
		public delegate int WriteCallback(IntPtr self, IntPtr ptr, int size, int n);
		public delegate int SeekCallback3(IntPtr self, long offset, int whence);
		public delegate long TellCallback3(IntPtr self);
		public delegate int FlushCallback(IntPtr self);
		public delegate int WriteCallback2(IntPtr self, IntPtr ptr, int size, int n);
		public delegate int SeekCallback4(IntPtr self, long offset, int whence);
		public delegate long TellCallback4(IntPtr self);
		public delegate int FlushCallback2(IntPtr self);
	}
	
}
