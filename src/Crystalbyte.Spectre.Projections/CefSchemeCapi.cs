using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefSchemeCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_register_scheme_handler_factory", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefRegisterSchemeHandlerFactory(IntPtr schemeName, IntPtr domainName, IntPtr factory);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_clear_scheme_handler_factories", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefClearSchemeHandlerFactories();
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefSchemeRegistrar {
		public CefBase Base;
		public IntPtr AddCustomScheme;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefSchemeHandlerFactory {
		public CefBase Base;
		public IntPtr Create;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefSchemeCapiDelegates {
		public delegate int AddCustomSchemeCallback(IntPtr self, IntPtr schemeName, int isStandard, int isLocal, int isDisplayIsolated);
		public delegate IntPtr CreateCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemeName, IntPtr request);
	}
	
}
