using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefWebPluginCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_visit_web_plugin_info", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void CefVisitWebPluginInfo(IntPtr visitor);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefWebPluginInfo {
		public CefBase Base;
		public IntPtr GetName;
		public IntPtr GetPath;
		public IntPtr GetVersion;
		public IntPtr GetDescription;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefWebPluginInfoVisitor {
		public CefBase Base;
		public IntPtr Visit;
	}
	
	public delegate IntPtr GetPathCallback(IntPtr self);
	public delegate IntPtr GetVersionCallback(IntPtr self);
	public delegate IntPtr GetDescriptionCallback(IntPtr self);
	
}
