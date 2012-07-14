using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
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
	
	public delegate int ReadCallback(IntPtr self, IntPtr ptr, int size, int n);
	public delegate int SeekCallback(IntPtr self, long offset, int whence);
	public delegate long TellCallback(IntPtr self);
	public delegate int EofCallback(IntPtr self);
	public delegate int WriteCallback(IntPtr self, IntPtr ptr, int size, int n);
	public delegate int FlushCallback(IntPtr self);
	
}
