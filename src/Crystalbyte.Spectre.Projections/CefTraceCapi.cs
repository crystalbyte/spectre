using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefTraceCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_begin_tracing", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefBeginTracing(IntPtr client, IntPtr categories);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_get_trace_buffer_percent_full_async", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefGetTraceBufferPercentFullAsync();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_end_tracing_async", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefEndTracingAsync();
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefTraceClient {
		public CefBase Base;
		public IntPtr OnTraceDataCollected;
		public IntPtr OnTraceBufferPercentFullReply;
		public IntPtr OnEndTracingComplete;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefTraceCapiDelegates {
		public delegate void OnTraceDataCollectedCallback(IntPtr self, IntPtr fragment, int fragmentSize);
		public delegate void OnTraceBufferPercentFullReplyCallback(IntPtr self, Float percentFull);
		public delegate void OnEndTracingCompleteCallback(IntPtr self);
	}
	
}
