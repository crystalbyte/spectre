using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefResourceBundleHandler {
		CefBase Base;
		IntPtr GetLocalizedString;
		IntPtr GetDataResource;
	}
	
	public delegate int GetLocalizedStringCallback(IntPtr self, int messageId, IntPtr @string);
	public delegate int GetDataResourceCallback(IntPtr self, int resourceId, IntPtr data, IntPtr dataSize);
	
}
