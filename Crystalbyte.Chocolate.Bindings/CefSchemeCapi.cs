using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefSchemeCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_register_custom_scheme", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefRegisterCustomScheme(IntPtr schemeName, int isStandard, int isLocal, int isDisplayIsolated);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_register_scheme_handler_factory", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefRegisterSchemeHandlerFactory(IntPtr schemeName, IntPtr domainName, IntPtr factory);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_clear_scheme_handler_factories", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefClearSchemeHandlerFactories();
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefSchemeHandlerFactory {
		CefBase Base;
		IntPtr Create;
	}
	
	public delegate IntPtr CreateCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemeName, IntPtr request);
	
}
