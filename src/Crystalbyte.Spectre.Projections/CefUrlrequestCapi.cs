using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefUrlrequestCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_urlrequest_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefUrlrequestCreate(IntPtr request, IntPtr client);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefUrlrequest {
		public CefBase Base;
		public IntPtr GetRequest;
		public IntPtr GetClient;
		public IntPtr GetRequestStatus;
		public IntPtr GetRequestError;
		public IntPtr GetResponse;
		public IntPtr Cancel;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefUrlrequestClient {
		public CefBase Base;
		public IntPtr OnRequestComplete;
		public IntPtr OnUploadProgress;
		public IntPtr OnDownloadProgress;
		public IntPtr OnDownloadData;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefUrlrequestCapiDelegates {
		public delegate IntPtr GetRequestCallback(IntPtr self);
		public delegate IntPtr GetClientCallback2(IntPtr self);
		public delegate CefUrlrequestStatus GetRequestStatusCallback(IntPtr self);
		public delegate CefErrorcode GetRequestErrorCallback(IntPtr self);
		public delegate IntPtr GetResponseCallback(IntPtr self);
		public delegate void CancelCallback7(IntPtr self);
		public delegate void OnRequestCompleteCallback(IntPtr self, IntPtr request);
		public delegate void OnUploadProgressCallback(IntPtr self, IntPtr request, ulong current, ulong total);
		public delegate void OnDownloadProgressCallback(IntPtr self, IntPtr request, ulong current, ulong total);
		public delegate void OnDownloadDataCallback(IntPtr self, IntPtr request, IntPtr data, int dataLength);
	}
	
}
