using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefZipReaderCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_zip_reader_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefZipReaderCreate(IntPtr stream);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefZipReader {
		CefBase Base;
		IntPtr MoveToFirstFile;
		IntPtr MoveToNextFile;
		IntPtr MoveToFile;
		IntPtr Close;
		IntPtr GetFileName;
		IntPtr GetFileSize;
		IntPtr GetFileLastModified;
		IntPtr OpenFile;
		IntPtr CloseFile;
		IntPtr ReadFile;
		IntPtr Tell;
		IntPtr Eof;
	}
	
	public delegate int MoveToFirstFileCallback(IntPtr self);
	public delegate int MoveToNextFileCallback(IntPtr self);
	public delegate int MoveToFileCallback(IntPtr self, IntPtr filename, int casesensitive);
	public delegate int CloseCallback(IntPtr self);
	public delegate IntPtr GetFileNameCallback(IntPtr self);
	public delegate long GetFileSizeCallback(IntPtr self);
	public delegate long GetFileLastModifiedCallback(IntPtr self);
	public delegate int OpenFileCallback(IntPtr self, IntPtr password);
	public delegate int CloseFileCallback(IntPtr self);
	public delegate int ReadFileCallback(IntPtr self, IntPtr buffer, int buffersize);
	
}
