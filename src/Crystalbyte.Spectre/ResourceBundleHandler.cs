using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Interop;

namespace Crystalbyte.Spectre
{
	public sealed class ResourceBundleHandler : OwnedRefCountedNativeTypeAdapter
	{
		private readonly CefResourceBundleHandlerCapiDelegates.GetDataResourceCallback _getDataResourceCallback;
		private readonly CefResourceBundleHandlerCapiDelegates.GetLocalizedStringCallback _getlocalizedStringCallback;
		//private readonly AppDelegate _appDelegate;

		public ResourceBundleHandler(AppDelegate appDelegate) 
			: base(typeof(CefResourceBundleHandler))
		{
			//_appDelegate = appDelegate;
			_getDataResourceCallback = OnGetDataResource;
			_getlocalizedStringCallback = OnGetLocalizedString;

			MarshalToNative(new CefResourceBundleHandler{
				Base = DedicatedBase,
				GetDataResource = Marshal.GetFunctionPointerForDelegate(_getDataResourceCallback),
				GetLocalizedString = Marshal.GetFunctionPointerForDelegate(_getlocalizedStringCallback)
			});
		}

		private int OnGetLocalizedString(IntPtr self, int messageId, IntPtr @string) {
			return 0;
		}

		private int OnGetDataResource(IntPtr self, int resourceId, IntPtr data, ref int dataSize) {
			return 0;
		}
	}
}

