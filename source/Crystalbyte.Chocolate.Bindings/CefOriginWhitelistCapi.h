using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefOriginWhitelistCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_add_cross_origin_whitelist_entry", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefAddCrossOriginWhitelistEntry(IntPtr sourceOrigin, IntPtr targetProtocol, IntPtr targetDomain, int allowTargetSubdomains);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_remove_cross_origin_whitelist_entry", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefRemoveCrossOriginWhitelistEntry(IntPtr sourceOrigin, IntPtr targetProtocol, IntPtr targetDomain, int allowTargetSubdomains);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_clear_cross_origin_whitelist", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefClearCrossOriginWhitelist();
	}
	
}
