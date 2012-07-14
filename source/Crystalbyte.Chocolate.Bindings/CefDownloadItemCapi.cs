using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefDownloadItem {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr IsInProgress;
		public IntPtr IsComplete;
		public IntPtr IsCanceled;
		public IntPtr GetCurrentSpeed;
		public IntPtr GetPercentComplete;
		public IntPtr GetTotalBytes;
		public IntPtr GetReceivedBytes;
		public IntPtr GetStartTime;
		public IntPtr GetEndTime;
		public IntPtr GetFullPath;
		public IntPtr GetId;
		public IntPtr GetUrl;
		public IntPtr GetSuggestedFileName;
		public IntPtr GetContentDisposition;
		public IntPtr GetMimeType;
		public IntPtr GetReferrerCharset;
	}
	
	public delegate int IsInProgressCallback(IntPtr self);
	public delegate int IsCompleteCallback(IntPtr self);
	public delegate int IsCanceledCallback(IntPtr self);
	public delegate long GetCurrentSpeedCallback(IntPtr self);
	public delegate int GetPercentCompleteCallback(IntPtr self);
	public delegate long GetTotalBytesCallback(IntPtr self);
	public delegate long GetReceivedBytesCallback(IntPtr self);
	public delegate IntPtr GetStartTimeCallback(IntPtr self);
	public delegate IntPtr GetEndTimeCallback(IntPtr self);
	public delegate IntPtr GetFullPathCallback(IntPtr self);
	public delegate int GetIdCallback(IntPtr self);
	public delegate IntPtr GetUrlCallback(IntPtr self);
	public delegate IntPtr GetSuggestedFileNameCallback(IntPtr self);
	public delegate IntPtr GetContentDispositionCallback(IntPtr self);
	public delegate IntPtr GetMimeTypeCallback(IntPtr self);
	public delegate IntPtr GetReferrerCharsetCallback(IntPtr self);
	
}
