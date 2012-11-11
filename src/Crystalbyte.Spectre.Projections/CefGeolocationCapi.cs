using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefGeolocationCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_get_geolocation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefGetGeolocation(IntPtr callback);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefGetGeolocationCallback {
		public CefBase Base;
		public IntPtr OnLocationUpdate;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefGeolocationCapiDelegates {
		public delegate void OnLocationUpdateCallback(IntPtr self, IntPtr position);
	}
	
}
