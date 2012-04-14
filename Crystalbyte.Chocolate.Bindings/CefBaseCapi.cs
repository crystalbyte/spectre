using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBase {
		public int Size;
		public IntPtr AddRef;
		public IntPtr Release;
		public IntPtr GetRefct;
	}
	
	public delegate int AddRefCallback(IntPtr self);
	public delegate int ReleaseCallback(IntPtr self);
	public delegate int GetRefctCallback(IntPtr self);
	
}
