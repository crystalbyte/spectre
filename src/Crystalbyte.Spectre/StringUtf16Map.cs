using System;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre
{
	public class StringUtf16Map : NativeObject
	{
		private StringUtf16Map (IntPtr handle)
		{
			NativeHandle = handle;				
		}

		public StringUtf16Map ()
			: this(CefStringMapClass.CefStringMapAlloc())
		{
			
		}

		public IntPtr NativeHandle {
			get;
			set;
		}
	}
}

