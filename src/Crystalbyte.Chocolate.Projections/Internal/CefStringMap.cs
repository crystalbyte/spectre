using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections.Internal
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefStringMapClass {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_alloc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringMapAlloc();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMapSize(IntPtr map);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_find", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMapFind(IntPtr map, IntPtr key, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_key", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMapKey(IntPtr map, int index, IntPtr key);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_value", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMapValue(IntPtr map, int index, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_append", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMapAppend(IntPtr map, IntPtr key, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_clear", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringMapClear(IntPtr map);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_free", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringMapFree(IntPtr map);
	}
	
}
