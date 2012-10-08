using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefZipReaderCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_zip_reader_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefZipReaderCreate(IntPtr stream);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefZipReader {
		public CefBase Base;
		public IntPtr MoveToFirstFile;
		public IntPtr MoveToNextFile;
		public IntPtr MoveToFile;
		public IntPtr Close;
		public IntPtr GetFileName;
		public IntPtr GetFileSize;
		public IntPtr GetFileLastModified;
		public IntPtr OpenFile;
		public IntPtr CloseFile;
		public IntPtr ReadFile;
		public IntPtr Tell;
		public IntPtr Eof;
	}
	
	public delegate int MoveToFirstFileCallback(IntPtr self);
	public delegate int MoveToNextFileCallback(IntPtr self);
	public delegate int MoveToFileCallback(IntPtr self, IntPtr filename, int casesensitive);
	public delegate IntPtr GetFileNameCallback(IntPtr self);
	public delegate long GetFileSizeCallback(IntPtr self);
	public delegate long GetFileLastModifiedCallback(IntPtr self);
	public delegate int OpenFileCallback(IntPtr self, IntPtr password);
	public delegate int CloseFileCallback(IntPtr self);
	public delegate int ReadFileCallback(IntPtr self, IntPtr buffer, int buffersize);
	
}
