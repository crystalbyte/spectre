using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
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
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefProcessMessageCapiDelegates {
		public delegate int IsValidCallback4(IntPtr self);
		public delegate int IsReadOnlyCallback2(IntPtr self);
		public delegate IntPtr CopyCallback3(IntPtr self);
		public delegate IntPtr GetNameCallback3(IntPtr self);
		public delegate IntPtr GetArgumentListCallback(IntPtr self);
	}
	
}
