using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefStringMultimapClass {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_alloc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringMultimapAlloc();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_size", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMultimapSize(IntPtr map);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_find_count", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMultimapFindCount(IntPtr map, IntPtr key);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_erate", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMultimapErate(IntPtr map, IntPtr key, int valueIndex, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_key", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMultimapKey(IntPtr map, int index, IntPtr key);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_value", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMultimapValue(IntPtr map, int index, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_append", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringMultimapAppend(IntPtr map, IntPtr key, IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_clear", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringMultimapClear(IntPtr map);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_free", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringMultimapFree(IntPtr map);
	}
	
}
