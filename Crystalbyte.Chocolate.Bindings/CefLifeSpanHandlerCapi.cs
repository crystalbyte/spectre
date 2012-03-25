using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefLifeSpanHandler {
		CefBase Base;
		IntPtr OnBeforePopup;
		IntPtr OnAfterCreated;
		IntPtr RunModal;
		IntPtr DoClose;
		IntPtr OnBeforeClose;
	}
	
	public delegate int OnBeforePopupCallback(IntPtr self, IntPtr parentbrowser, IntPtr popupfeatures, IntPtr windowinfo, IntPtr url, IntPtr client, IntPtr settings);
	public delegate void OnAfterCreatedCallback(IntPtr self, IntPtr browser);
	public delegate int RunModalCallback(IntPtr self, IntPtr browser);
	public delegate int DoCloseCallback(IntPtr self, IntPtr browser);
	public delegate void OnBeforeCloseCallback(IntPtr self, IntPtr browser);
	
}
