using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections.Internal
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefTimeClass {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_time_to_timet", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefTimeToTimet(IntPtr cefTime, IntPtr time);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_time_from_timet", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefTimeFromTimet(long time, IntPtr cefTime);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_time_to_doublet", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefTimeToDoublet(IntPtr cefTime, IntPtr time);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_time_from_doublet", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefTimeFromDoublet(Double time, IntPtr cefTime);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_time_now", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefTimeNow(IntPtr cefTime);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefTime {
		public int Year;
		public int Month;
		public int DayOfWeek;
		public int DayOfMonth;
		public int Hour;
		public int Minute;
		public int Second;
		public int Millisecond;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefTimeDelegates {
	}
	
}
