using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefResponse {
		public CefBase Base;
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
	
	public delegate int GetStatusCallback(IntPtr self);
	public delegate void SetStatusCallback(IntPtr self, int status);
	public delegate IntPtr GetStatusTextCallback(IntPtr self);
	public delegate void SetStatusTextCallback(IntPtr self, IntPtr statustext);
	public delegate IntPtr GetMimeTypeCallback(IntPtr self);
	public delegate void SetMimeTypeCallback(IntPtr self, IntPtr mimetype);
	public delegate IntPtr GetHeaderCallback(IntPtr self, IntPtr name);
	
}
