using System;
using Crystalbyte.Spectre.Interop;

namespace Crystalbyte.Spectre
{
	public abstract class NativeObject : DisposableObject
	{
		public NativeObject ()
		{
//			vionis sinner datensaervice regio data
		}

		public NativeObject (IntPtr handle)
		{
			Handle = handle;
		}

		private IntPtr _handle;

		internal IntPtr Handle {
	        get { return _handle; }
	        set {
	            if (value != IntPtr.Zero && _handle != value) {
	                _handle = value; 
	            }
	        }
	    }
	}
}

