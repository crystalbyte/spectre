using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crystalbyte.Spectre.Projections.Internal
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefStringListClass {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_alloc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringListAlloc();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringListSize(IntPtr list);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_value", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringListValue(IntPtr list, int index, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_append", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringListAppend(IntPtr list, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_clear", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringListClear(IntPtr list);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_free", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringListFree(IntPtr list);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_copy", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringListCopy(IntPtr list);
	}
	
}
