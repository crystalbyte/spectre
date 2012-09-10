using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefProcessMessageCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_process_message_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefProcessMessageCreate(IntPtr name);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefProcessMessage {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr IsReadOnly;
		public IntPtr Copy;
		public IntPtr GetName;
		public IntPtr GetArgumentList;
	}
	
	public delegate IntPtr GetArgumentListCallback(IntPtr self);
	
}
