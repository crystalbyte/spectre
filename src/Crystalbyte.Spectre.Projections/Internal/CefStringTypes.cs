using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crystalbyte.Spectre.Projections.Internal
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefStringTypesClass {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_wide_set", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringWideSet(IntPtr src, int srcLen, IntPtr output, int copy);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf8_set", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf8Set(IntPtr src, int srcLen, IntPtr output, int copy);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf16_set", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf16Set(IntPtr src, int srcLen, IntPtr output, int copy);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_wide_clear", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringWideClear(IntPtr str);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf8_clear", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringUtf8Clear(IntPtr str);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf16_clear", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringUtf16Clear(IntPtr str);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_wide_cmp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringWideCmp(IntPtr str1, IntPtr str2);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf8_cmp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf8Cmp(IntPtr str1, IntPtr str2);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf16_cmp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf16Cmp(IntPtr str1, IntPtr str2);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_wide_to_utf8", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringWideToUtf8(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf8_to_wide", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf8ToWide(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_wide_to_utf16", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringWideToUtf16(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf16_to_wide", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf16ToWide(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf8_to_utf16", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf8ToUtf16(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_utf16_to_utf8", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringUtf16ToUtf8(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_ascii_to_wide", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringAsciiToWide(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_ascii_to_utf16", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefStringAsciiToUtf16(IntPtr src, int srcLen, IntPtr output);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_userfree_wide_alloc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringUserfreeWideAlloc();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_userfree_utf8_alloc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringUserfreeUtf8Alloc();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_userfree_utf16_alloc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefStringUserfreeUtf16Alloc();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_userfree_wide_free", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringUserfreeWideFree(IntPtr str);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_userfree_utf8_free", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringUserfreeUtf8Free(IntPtr str);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_string_userfree_utf16_free", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefStringUserfreeUtf16Free(IntPtr str);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefStringWide {
		public IntPtr Str;
		public int Length;
		public IntPtr Dtor;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefStringUtf8 {
		public IntPtr Str;
		public int Length;
		public IntPtr Dtor;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefStringUtf16 {
		public IntPtr Str;
		public int Length;
		public IntPtr Dtor;
	}
	
	public delegate void WcharCallback(IntPtr str);
	public delegate void CharCallback(IntPtr str);
	public delegate void Char16Callback(IntPtr str);
	
}
