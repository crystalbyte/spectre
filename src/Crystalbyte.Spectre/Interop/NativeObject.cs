using System;

namespace Crystalbyte.Spectre.Interop
{
	public abstract class NativeObject : DisposableObject
	{
	    protected NativeObject ()
		{

		}

	    protected NativeObject (IntPtr handle)
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

