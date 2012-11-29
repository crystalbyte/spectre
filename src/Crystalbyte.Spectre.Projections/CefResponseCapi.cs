using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefResponseCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_response_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefResponseCreate();
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefResponse {
		public CefBase Base;
		public IntPtr IsReadOnly;
		public IntPtr GetStatus;
		public IntPtr SetStatus;
		public IntPtr GetStatusText;
		public IntPtr SetStatusText;
		public IntPtr GetMimeType;
		public IntPtr SetMimeType;
		public IntPtr GetHeader;
		public IntPtr GetHeaderMap;
		public IntPtr SetHeaderMap;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefResponseCapiDelegates {
		public delegate int IsReadOnlyCallback6(IntPtr self);
		public delegate int GetStatusCallback(IntPtr self);
		public delegate void SetStatusCallback(IntPtr self, int status);
		public delegate IntPtr GetStatusTextCallback(IntPtr self);
		public delegate void SetStatusTextCallback(IntPtr self, IntPtr statustext);
		public delegate IntPtr GetMimeTypeCallback2(IntPtr self);
		public delegate void SetMimeTypeCallback(IntPtr self, IntPtr mimetype);
		public delegate IntPtr GetHeaderCallback(IntPtr self, IntPtr name);
		public delegate void GetHeaderMapCallback2(IntPtr self, IntPtr headermap);
		public delegate void SetHeaderMapCallback2(IntPtr self, IntPtr headermap);
	}
	
}
